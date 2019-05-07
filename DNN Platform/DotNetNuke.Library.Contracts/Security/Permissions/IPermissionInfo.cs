namespace DotNetNuke.Library.Contracts.Security.Permissions
{
    public interface IPermissionInfo
    {
        int ModuleDefID { get; set; }
        string PermissionCode { get; set; }
        int PermissionID { get; set; }
        string PermissionKey { get; set; }
        string PermissionName { get; set; }
    }
}
