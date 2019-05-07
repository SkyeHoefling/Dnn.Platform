namespace DotNetNuke.Library.Contracts.Security.Permissions
{
    public interface IModulePermissionInfo
    {
        bool AllowAccess { get; set; }
        string DisplayName { get; set; }
        int RoleID { get; set; }
        string RoleName { get; set; }
        int UserID { get; set; }
        string Username { get; set; }
    }
}
