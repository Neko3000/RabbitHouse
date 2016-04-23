using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using RabbitHouse.Models;
using AccountAdmin.ViewModels;

using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;

using System.Net;

using PagedList;

namespace SchoolLibrary.Controllers
{
    public class RolesAdminController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public ApplicationDbContext context { get; private set; }

        public RolesAdminController()
        {
            context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        public RolesAdminController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        //Get: /RolesAdmin/
        public async Task<ActionResult> Index(int? page)
        {
            var pageSize = 10;
            var pageNumber = page ?? 1;
            var onePageOfRoles = (await RoleManager.Roles.ToListAsync()).ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfUserRolesGroup = onePageOfRoles;

            var indexVM = new RolesAdminIndexViewModel
            {
                Roles = onePageOfRoles
            };
            return View(indexVM);
        }

        //Get: /RolesAdmin/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = await RoleManager.FindByIdAsync(id);

            var users = new List<ApplicationUser>();
            foreach (var user in role.Users)
            {
                users.Add(await UserManager.FindByIdAsync(user.UserId));
            }

            var detailsVM = new RolesAdminDetailsViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Users = users
            };

            return View(detailsVM);
        }

        //Get: /RolesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post: /RolesAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create(RolesAdminCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var role = new IdentityRole(model.RoleName);
                var roleResult = await RoleManager.CreateAsync(role);
                if(!roleResult.Succeeded)
                {
                    ModelState.AddModelError("", roleResult.Errors.First().ToString());
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //Get: /RolesAdmin/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if(role==null)
            {
                return HttpNotFound();
            }

            var editVM = new RolesAdminEditViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            return View(editVM);
        }

        //Post: /RolesAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RolesAdminEditViewModel model)
        {
            var tempIdentityRole = new IdentityRole
            {
                Id = model.RoleId,
                Name = model.RoleName
            };

            if(ModelState.IsValid)
            {
                var result = await RoleManager.UpdateAsync(tempIdentityRole);
                if(!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());
                    return View();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //Get: /RolesAdmin/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role=await RoleManager.FindByIdAsync(id);
            if(role==null)
            {
                return HttpNotFound();
            }

            var deleteVM = new RolesAdminDeleteViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            return View(deleteVM);
        }

        //Get: /RolesAdmin/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(RolesAdminDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.RoleId==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(model.RoleId);
                var result = await RoleManager.DeleteAsync(role);
                if(!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());
                    return View();
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