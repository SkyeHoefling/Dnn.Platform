using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetNuke.Library.Contracts.Security.Permissions
{
    public interface IPermissionInfoBase : IPermissionInfo
    {
        bool AllowAccess { get; set; }
        string DisplayName { get; set; }
        int RoleID { get; set; }
        string RoleName { get; set; }
        int UserID { get; set; }
        string Username { get; set; }
    }
}
