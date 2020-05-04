﻿using System;
using DotNetNuke.Common;
using DotNetNuke.Framework;
using DotNetNuke.Services.FileSystem;

namespace Dnn.Modules.ResourceManager.Components
{
    public class GroupManager : ServiceLocator<IGroupManager, GroupManager>, IGroupManager
    {
        protected override Func<IGroupManager> GetFactory()
        {
            return () => new GroupManager();
        }

        public IFolderInfo FindOrCreateGroupFolder(int portalId, int groupId)
        {
            Requires.NotNegative("portalId", portalId);
            Requires.NotNegative("groupId", groupId);

            return GetGroupFolder(portalId, groupId) ?? FolderManager.Instance.AddFolder(portalId, "Groups/" + groupId);
        }

        public IFolderInfo GetGroupFolder(int portalId, int groupId)
        {
            Requires.NotNegative("portalId", portalId);
            Requires.NotNegative("groupId", groupId);

            return FolderManager.Instance.GetFolder(portalId, "Groups/" + groupId);
        }
    }
}