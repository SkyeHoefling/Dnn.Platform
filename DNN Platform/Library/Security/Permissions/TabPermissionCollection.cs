#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2018
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Library.Contracts.Security.Permissions;

#endregion

namespace DotNetNuke.Security.Permissions
{
    /// -----------------------------------------------------------------------------
    /// Project	 : DotNetNuke
    /// Namespace: DotNetNuke.Security.Permissions
    /// Class	 : TabPermissionCollection
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// TabPermissionCollection provides the a custom collection for TabPermissionInfo
    /// objects
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable]
    [XmlRoot("tabpermissions")]
    public class TabPermissionCollection : CollectionBase, ITabPermissionCollection
    {
        public TabPermissionCollection()
        {
        }

        public TabPermissionCollection(ArrayList tabPermissions)
        {
            AddRange(tabPermissions);
        }

        public TabPermissionCollection(TabPermissionCollection tabPermissions)
        {
            AddRange(tabPermissions);
        }

        public TabPermissionCollection(ArrayList tabPermissions, int TabId)
        {
            foreach (TabPermissionInfo permission in tabPermissions)
            {
                if (permission.TabID == TabId)
                {
                    Add(permission);
                }
            }
        }

        public ITabPermissionInfo this[int index]
        {
            get
            {
                return (ITabPermissionInfo) List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(ITabPermissionInfo value)
        {
            return List.Add(value);
        }

        public int Add(ITabPermissionInfo value, bool checkForDuplicates)
        {
            int id = Null.NullInteger;

            if (!checkForDuplicates)
            {
                id = Add(value);
            }
            else
            {
                bool isMatch = false;
                foreach (IPermissionInfoBase permission in List)
                {
                    if (permission.PermissionID == value.PermissionID && permission.UserID == value.UserID && permission.RoleID == value.RoleID)
                    {
                        isMatch = true;
                        break;
                    }
                }
                if (!isMatch)
                {
                    id = Add(value);
                }
            }

            return id;
        }

        public void AddRange(ArrayList tabPermissions)
        {
            foreach (ITabPermissionInfo permission in tabPermissions)
            {
                Add(permission);
            }
        }

        public void AddRange(IEnumerable<ITabPermissionInfo> tabPermissions)
        {
            foreach (ITabPermissionInfo permission in tabPermissions)
            {
                Add(permission);
            }
        }

        public void AddRange(ITabPermissionCollection tabPermissions)
        {
            foreach (ITabPermissionInfo permission in tabPermissions)
            {
                Add(permission);
            }
        }

        public bool CompareTo(TabPermissionCollection objTabPermissionCollection)
        {
            if (objTabPermissionCollection.Count != Count)
            {
                return false;
            }
            InnerList.Sort(new CompareTabPermissions());
            objTabPermissionCollection.InnerList.Sort(new CompareTabPermissions());
            for (int i = 0; i <= Count - 1; i++)
            {
                if (objTabPermissionCollection[i].TabPermissionID != this[i].TabPermissionID
                        || objTabPermissionCollection[i].PermissionID != this[i].PermissionID
                        || objTabPermissionCollection[i].RoleID != this[i].RoleID
                        || objTabPermissionCollection[i].UserID != this[i].UserID
                        || objTabPermissionCollection[i].AllowAccess != this[i].AllowAccess)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Contains(ITabPermissionInfo value)
        {
            return List.Contains(value);
        }

        public int IndexOf(ITabPermissionInfo value)
        {
            return List.IndexOf(value);
        }

        public void Insert(int index, ITabPermissionInfo value)
        {
            List.Insert(index, value);
        }

        public void Remove(ITabPermissionInfo value)
        {
            List.Remove(value);
        }

        public void Remove(int permissionID, int roleID, int userID)
        {
            foreach (IPermissionInfoBase permission in List)
            {
                if (permission.PermissionID == permissionID && permission.UserID == userID && permission.RoleID == roleID)
                {
                    List.Remove(permission);
                    break;
                }
            }
        }

        public List<IPermissionInfoBase> ToList()
        {
            var list = new List<IPermissionInfoBase>();
            foreach (IPermissionInfoBase permission in List)
            {
                list.Add(permission);
            }
            return list;
        }

        public string ToString(string key)
        {
            return PermissionController.BuildPermissions(List, key);
        }

        public IEnumerable<ITabPermissionInfo> Where(Func<ITabPermissionInfo, bool> predicate)
        {
            return this.Cast<TabPermissionInfo>().Where(predicate);
        }
    }
}
