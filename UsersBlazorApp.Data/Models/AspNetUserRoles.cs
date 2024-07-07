using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersBlazorApp.Data.Models;

namespace UsersBlazorApp.Data.Models;

public partial class AspNetUserRoles
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public AspNetUsers User { get; set; }
    public AspNetRoles Role { get; set; }

}