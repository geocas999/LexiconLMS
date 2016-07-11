using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace LexiconLMS.Models
{

    public class UsersViewModel
    {
        public IPagedList<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }


}