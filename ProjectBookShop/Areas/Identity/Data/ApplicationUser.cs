﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProjectBookShop.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ProjectBookShopUser class
    public class ApplicationUser : IdentityUser
    {
        public string? FrisName {get;set;}
        public String? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Image { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastVisitDateTime { get; set; }
        public bool  IsActive { get; set; }
        public virtual List<ApplicationUserRole>? Roles { get; set; }
    }
}
