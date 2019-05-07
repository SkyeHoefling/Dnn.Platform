using System;
using System.Collections;
using System.Collections.Generic;

namespace DotNetNuke.Library.Contracts.Security.Permissions
{
    public interface ITabPermissionCollection : IEnumerable
    {
        ITabPermissionInfo this[int index] { get; set; }
        int Add(ITabPermissionInfo value);
        int Add(ITabPermissionInfo value, bool checkForDuplicates);
        void AddRange(ArrayList tabPermissions);
        void AddRange(IEnumerable<ITabPermissionInfo> tabPermissions);
        void AddRange(ITabPermissionCollection tabPermissions);
        //bool CompareTo(ITabPermissionCollection objTabPermissionCollection);
        bool Contains(ITabPermissionInfo value);
        int IndexOf(ITabPermissionInfo value);
        void Insert(int index, ITabPermissionInfo value);
        void Remove(ITabPermissionInfo value);
        void Remove(int permissionID, int roleID, int userID);
        List<IPermissionInfo> ToList();
        string ToString(string key);
        IEnumerable<ITabPermissionInfo> Where(Func<ITabPermissionInfo, bool> predicate);
    }
}
