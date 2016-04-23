using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SchoolLibrary.Models;

using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;

using System.Net;

using PagedList;

namespace SchoolLibrary.Controllers
{
    public class UsersAdminController : Controller
    {
        //
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get;private set; }
        public ApplicationDbContext context { get; private set; }
        //
        public UsersAdminController()
        {
            context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        //Get: /Users/
        public async Task<ActionResult> Index(int? page)
        {
            var userRolesGroupList = new List<UserRolesGroup>();
            var users = await UserManager.Users.ToListAsync();
            foreach(var item in users)
            {
                var singleUserRolesGroup = new UserRolesGroup
                {
                    User = item,
                    RolesForUser = UserManager.GetRoles(item.Id)
                };
                userRolesGroupList.Add(singleUserRolesGroup);
            }

            var pageSize = 10;
            var pageNumber = page ?? 1;
            var onePageOfUserRolesGroup = userRolesGroupList.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfUserRolesGroup = onePageOfUserRolesGroup;
            var indexVM = new UsersAdminIndexViewModel
            {
                Users = onePageOfUserRolesGroup
            };
            return View(indexVM);
        }
        //Get: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            var detailsVM = new UsersAdminDetailsViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RolesNameForUser = new List<string>(await UserManager.GetRolesAsync(user.Id)).ToArray(),
                Logins = user.Logins
            };

            return View(detailsVM);
        }
        //Get: /User/Create
        public async Task<ActionResult> Create()
        {
            //get the list of the roles
            var createVM = new UsersAdminCreateViewModel
            {
                Roles = await RoleManager.Roles.ToListAsync()
            };

            return View(createVM);
        }

        //Post: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(UsersAdminCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = model.UserName;
                user.Email = model.Email;

                var adminResult = await UserManager.CreateAsync(user, model.Password);
                if(adminResult.Succeeded)
                {
                    if(model.RolesIdForUser.Count()!=0)
                    {
                        //set Role for each RoleId
                        foreach(var item in model.RolesIdForUser)
                        {
                            var role = await RoleManager.FindByIdAsync(item);
                            var result = await UserManager.AddToRoleAsync(user.Id, role.Name);

                            //failed
                            if (!result.Succeeded)
                            {
                                //failed to set role for user
                                ModelState.AddModelError("", result.Errors.First().ToString());
                                return View();                             
                            }
                        }                    
                    }
                }
                else
                {
                    //failed to create user
                    ModelState.AddModelError("", adminResult.Errors.First().ToString());     
                    return View();
                }
                //succeed to add a new user
                return RedirectToAction("Index");
            }
            else
            {
                //Note-Since we do not use ViewModel instead of ViewBag to pass Roles data to View
                //So when the post of the form failed.Need Not To Rebuild Roles data

                //var createVM = new UsersAdminCreateViewModel
                //{
                //    Roles = new SelectList(await RoleManager.Roles.ToListAsync(), "Id", "Name")
                //};
                //ViewBag.RoleId = new SelectList(RoleManager.Roles, "Id", "Name");

                //failed to add
                return View();
            }

        }

        //Get: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        
            var user = await UserManager.FindByIdAsync(id);
            if(user==null)
            {
                return HttpNotFound();
            }

            //for every RoleName find it's RoleId
            var rolesNameList = await UserManager.GetRolesAsync(user.Id);
            var rolesIdList = new List<string>();
            foreach(var item in rolesNameList)
            {
                rolesIdList.Add(RoleManager.FindByName(item).Id);
            }
            
            var editVM = new UsersAdminEditViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email=user.Email,
                PhoneNumber = user.PhoneNumber,
                RolesIdForUser = rolesIdList.ToArray(),
                Roles = await RoleManager.Roles.ToListAsync()
            };

            return View(editVM);
        }

        //Get: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UsersAdminEditViewModel model)
        {
            if (model.UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);              
            }

            var user = await UserManager.FindByIdAsync(model.UserId);
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;

            if (ModelState.IsValid)
            {
                //update the user details
                await UserManager.UpdateAsync(user);

                //if user has existing Role then remove the user from the role
                //this also accounts for the case when the Admin selected Empty from the drop-down and
                //this means that all roles for the user must be removed
                var currentRolesForUser = await UserManager.GetRolesAsync(user.Id);
                if (currentRolesForUser.Count() > 0)
                {
                    foreach (var item in currentRolesForUser)
                    {
                        var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                    }
                }

                if (model.RolesIdForUser!=null)
                {
                    foreach(var item in model.RolesIdForUser)
                    {
                        //find role
                        var role = await RoleManager.FindByIdAsync(item);
                        //add user to new role
                        var result = await UserManager.AddToRoleAsync(user.Id, role.Name);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First().ToString());
                            return View();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }      
    }
}