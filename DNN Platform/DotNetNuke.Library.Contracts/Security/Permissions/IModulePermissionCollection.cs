using System.Collections;

namespace DotNetNuke.Library.Contracts.Security.Permissions
{
    // TODO - try and add properties from original entity back in
    public interface IModulePermissionCollection : IEnumerable
    {
        IModulePermissionInfo this[int index] { get; set; }
        string ToString(string key);
    }
}
