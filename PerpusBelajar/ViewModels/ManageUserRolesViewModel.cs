using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerpusBelajar.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
