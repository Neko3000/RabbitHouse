using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using RabbitHouse.Models;

namespace AccountAdmin.ViewModels
{
    //User Admin
    //for show each user's multiole Roles in Index
    public class UserRolesGroup
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<string> RolesForUser { get; set; }
    }
    //Index
    public class UsersAdminIndexViewModel
    {
        public IEnumerable<UserRolesGroup> Users { get; set; }
    }
    //Create-post
    public class UsersAdminCreateViewModel
    {
        [Required]
        [Display(Name = "账户名")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }

        [Display(Name = "账户权限ID")]
        public string[] RolesIdForUser { get; set; }

        [Display(Name = "账户权限列表")]
        public  IEnumerable<IdentityRole> Roles { get; set; }

    }
    //Details
    public class UsersAdminDetailsViewModel
    {
        [Display(Name = "账户ID")]
        public string UserId { get; set; }

        [Display(Name ="账户名")]
        public string UserName { get; set; }

        [Display(Name ="电子邮件")]
        public string Email { get; set; }

        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }

        [Display(Name = "账户权限名称")]
        public string[] RolesNameForUser { get; set; }

        public ICollection<IdentityUserLogin> Logins { get; set; }
    }
    //Edit-get/post
    public class UsersAdminEditViewModel
    {
        [Required]
        [Display(Name = "账户ID")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "账户名")]
        public string UserName { get; set; }

        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }

        [Display(Name = "账户权限ID")]
        public string[] RolesIdForUser { get; set; }

        [Display(Name = "账户权限列表")]
        public IEnumerable<IdentityRole> Roles { get; set; }
    }

    //RoleAdmin
    //Index
    public class RolesAdminIndexViewModel
    {
        [Display(Name = "权限列表")]
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
    //Create
    public class RolesAdminCreateViewModel
    {
        [Required]
        [Display(Name="权限名称")]
        public string RoleName{ get; set; }
    }
    //Details
    public class RolesAdminDetailsViewModel
    {
        [Display(Name ="权限ID")]
        public string RoleId { get; set; }

        [Display(Name ="权限名称")]
        public string RoleName { get; set; }

        [Display(Name ="账户列表")]
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
    //Edit
    public class RolesAdminEditViewModel
    {
        [Required]
        [Display(Name ="权限ID")]
        public string RoleId { get; set; }

        [Required]
        [Display(Name ="权限名称")]
        public string RoleName { get; set; }
    }
    //Delete
    public class RolesAdminDeleteViewModel
    {
        [Required]
        [Display(Name = "权限ID")]
        public string RoleId { get; set; }

        [Display(Name = "权限名称")]
        public string RoleName { get; set; }
    }




    public class RoleViewModel
    {
        public string Name { get; set; }
    }

    //Edit-post
    public class RoleDetailsViewModel
    {
        public IdentityRole Role { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}