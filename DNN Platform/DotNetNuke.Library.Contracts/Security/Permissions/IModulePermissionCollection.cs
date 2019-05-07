using System;
using System.Collections;
using System.Collections.Generic;

namespace DotNetNuke.Library.Contracts.Security.Permissions
{
    public interface IModulePermissionCollection
    {
        IModulePermissionInfo this[int index] { get; set; }
        int Add(IModulePermissionInfo value);
        int Add(IModulePermissionInfo value, bool checkForDuplicates);
        void AddRange(ArrayList modulePermissions);
        void AddRange(IModulePermissionCollection modulePermissions);
        bool CompareTo(IModulePermissionCollection objModulePermissionCollection);
        bool Contains(IModulePermissionInfo value);
        int IndexOf(IModulePermissionInfo value);
        void Insert(int index, IModulePermissionInfo value);
        void Remove(IModulePermissionInfo value);
        void Remove(int permissionID, int roleID, int userID);
        List<IPermissionInfo> ToList();
        string ToString(string key);
        IEnumerable<IModulePermissionInfo> Where(Func<IModulePermissionInfo, bool> predicate);
    }
}
