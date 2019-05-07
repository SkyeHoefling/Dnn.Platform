namespace DotNetNuke.Library.Contracts.Security.Permissions
{
    public interface ITabPermissionInfo : IPermissionInfoBase
    {
        int TabPermissionID { get; set; }
        int TabID { get; set; }
    }
}
