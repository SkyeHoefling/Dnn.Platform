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
using System.Runtime.Serialization;
using System.Security;
using DotNetNuke.Collections;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Library.Contracts.Entities.Tabs;
using DotNetNuke.Services.Localization;

#endregion

namespace DotNetNuke.Entities.Tabs
{
    /// <summary>
    /// Represents the collection of Tabs for a portal
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class TabCollection : Dictionary<int, ITabInfo>
    {
		//This is used to provide a collection of children
        [NonSerialized]
        private readonly Dictionary<int, List<ITabInfo>> _children;

        //This is used to return a sorted List
        [NonSerialized]
        private readonly List<ITabInfo> _list;
        
        //This is used to provide a culture based set of tabs
        [NonSerialized]
        private readonly Dictionary<String, List<ITabInfo>> _localizedTabs;

        #region Constructors

        public TabCollection()
        {
            _list = new List<ITabInfo>();
            _children = new Dictionary<int, List<ITabInfo>>();
            _localizedTabs = new Dictionary<string, List<ITabInfo>>();
        }

        // The special constructor is used to deserialize values.
        public TabCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _list = new List<ITabInfo>();
            _children = new Dictionary<int, List<ITabInfo>>();
            _localizedTabs = new Dictionary<string, List<ITabInfo>>();
        }

        public override void OnDeserialization(object sender)
        {
            base.OnDeserialization(sender);

            foreach (var tab in Values)
            {
                //Update all child collections
                AddInternal(tab);
            }
        }

        public TabCollection(IEnumerable<ITabInfo> tabs) : this()
        {
            AddRange(tabs);
        }

        #endregion

        #region Private Methods

        private void AddInternal(ITabInfo tab)
        {
            if (tab.ParentId == Null.NullInteger)
            {
                //Add tab to Children collection
                AddToChildren(tab);

                //Add to end of List as all zero-level tabs are returned in order first
                _list.Add(tab);
            }
            else
            {
                //Find Parent in list
                for (int index = 0; index <= _list.Count - 1; index++)
                {
                    ITabInfo parentTab = _list[index];
                    if (parentTab.TabID == tab.ParentId)
                    {
                        int childCount = AddToChildren(tab);

                        //Insert tab in master List
                        _list.Insert(index + childCount, tab);
                    }
                }
            }
            //Add to localized tabs
            if (tab.PortalID == Null.NullInteger || IsLocalizationEnabled(tab.PortalID))
            {
                AddToLocalizedTabs(tab);
            }            
        }

        private int AddToChildren(ITabInfo tab)
        {
            List<ITabInfo> childList;
            if (!_children.TryGetValue(tab.ParentId, out childList))
            {
                childList = new List<ITabInfo>();
                _children.Add(tab.ParentId, childList);
            }
			
            //Add tab to end of child list as children are returned in order
            childList.Add(tab);
            return childList.Count;
        }

        private void AddToLocalizedTabCollection(ITabInfo tab, string cultureCode)
        {
            List<ITabInfo> localizedTabCollection;

            var key = cultureCode.ToLowerInvariant();
            if (!_localizedTabs.TryGetValue(key, out localizedTabCollection))
            {
                localizedTabCollection = new List<ITabInfo>();
                _localizedTabs.Add(key, localizedTabCollection);
            }

            //Add tab to end of localized tabs
            localizedTabCollection.Add(tab);
        }

        private void AddToLocalizedTabs(ITabInfo tab)
        {
            if (string.IsNullOrEmpty(tab.CultureCode))
            {
                //Add to all cultures
                foreach (var locale in LocaleController.Instance.GetLocales(tab.PortalID).Values)
                {
                    AddToLocalizedTabCollection(tab, locale.Code);
                }
            }
            else
            {
                AddToLocalizedTabCollection(tab, tab.CultureCode);
            }
        }

        private List<ITabInfo> GetDescendants(int tabId, int tabLevel)
        {
            var descendantTabs = new List<ITabInfo>();
            for (int index = 0; index <= _list.Count - 1; index++)
            {
                ITabInfo parentTab = _list[index];
                if (parentTab.TabID == tabId)
                {
                    //Found Parent - so add descendents
                    for (int descendantIndex = index + 1; descendantIndex <= _list.Count - 1; descendantIndex++)
                    {
                        ITabInfo descendantTab = _list[descendantIndex];

                        if ((tabLevel == Null.NullInteger))
                        {
                            tabLevel = parentTab.Level;
                        }

                        if (descendantTab.Level > tabLevel)
                        {
                            //Descendant so add to collection
                            descendantTabs.Add(descendantTab);
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                }
            }
            return descendantTabs;
        }

        private static bool IsLocalizationEnabled()
        {
            var portalSettings = PortalController.Instance.GetCurrentPortalSettings();
            return (portalSettings != null) ? portalSettings.ContentLocalizationEnabled : Null.NullBoolean;
        }

        private static bool IsLocalizationEnabled(int portalId)
        {
            return PortalController.GetPortalSettingAsBoolean("ContentLocalizationEnabled", portalId, false);
        }

        #endregion

        #region Public Methods

        public void Add(ITabInfo tab)
        {
			//Call base class to add to base Dictionary
            Add(tab.TabID, tab);

            //Update all child collections
            AddInternal(tab);
        }

        public void AddRange(IEnumerable<ITabInfo> tabs)
        {
            foreach (ITabInfo tab in tabs)
            {
                Add(tab);
            }
        }

        public List<ITabInfo> AsList()
        {
            return _list;
        }

        public List<ITabInfo> DescendentsOf(int tabId)
        {
            return GetDescendants(tabId, Null.NullInteger);
        }

        public List<ITabInfo> DescendentsOf(int tabId, int originalTabLevel)
        {
            return GetDescendants(tabId, originalTabLevel);
        }

        public bool IsDescendentOf(int ancestorId, int testTabId)
        {
            return DescendentsOf(ancestorId).Any(tab => tab.TabID == testTabId);
        }

        public ArrayList ToArrayList()
        {
            return new ArrayList(_list);
        }

		public TabCollection WithCulture(string cultureCode, bool includeNeutral)
		{
			return WithCulture(cultureCode, includeNeutral, IsLocalizationEnabled());
		}
        public TabCollection WithCulture(string cultureCode, bool includeNeutral, bool localizationEnabled)
        {
            TabCollection collection;
			if (localizationEnabled)
            {
                if (string.IsNullOrEmpty(cultureCode))
                {
                    //No culture passed in - so return all tabs
                    collection = this;
                }
                else
                {
                    cultureCode = cultureCode.ToLowerInvariant();
                    List<ITabInfo> tabs;
                    if (!_localizedTabs.TryGetValue(cultureCode, out tabs))
                    {
                        collection = new TabCollection(new List<ITabInfo>());
                    }
                    else
                    {
                        collection = !includeNeutral 
                                        ? new TabCollection(from t in tabs 
                                                            where t.CultureCode.ToLowerInvariant() == cultureCode
                                                            select t) 
                                        : new TabCollection(tabs);
                    }
                }
            }
            else
            {
                //Return all tabs
                collection = this;
            }
            return collection;
        }

        public List<ITabInfo> WithParentId(int parentId)
        {
            List<ITabInfo> tabs;
            if (!_children.TryGetValue(parentId, out tabs))
            {
                tabs = new List<ITabInfo>();
            }
            return tabs;
        }

        public ITabInfo WithTabId(int tabId)
        {
            ITabInfo t = null;
            if (ContainsKey(tabId))
            {
                t = this[tabId];
            }
            return t;
        }

        public ITabInfo WithTabNameAndParentId(string tabName, int parentId)
        {
            return (from t in _list where t.TabName.Equals(tabName, StringComparison.InvariantCultureIgnoreCase) && t.ParentId == parentId select t).SingleOrDefault();
        }

        public ITabInfo WithTabName(string tabName)
        {
            return (from t in _list where !string.IsNullOrEmpty(t.TabName) && t.TabName.Equals(tabName, StringComparison.InvariantCultureIgnoreCase) select t).FirstOrDefault();
        }

        internal void RefreshCache(int tabId, ITabInfo updatedTab)
        {
            if (ContainsKey(tabId))
            {
                if (updatedTab == null) //the tab has been deleted
                {
                    Remove(tabId);
                    _list.RemoveAll(t => t.TabID == tabId);
                    _localizedTabs.ForEach(kvp =>
                    {
                        kvp.Value.RemoveAll(t => t.TabID == tabId);
                    });
                    _children.Remove(tabId);
                }
                else
                {
                    this[tabId] = updatedTab;
                    var index = _list.FindIndex(t => t.TabID == tabId);
                    if (index > Null.NullInteger)
                    {
                        _list[index] = updatedTab;
                    }

                    _localizedTabs.ForEach(kvp =>
                    {
                        var localizedIndex = kvp.Value.FindIndex(t => t.TabID == tabId);
                        if (localizedIndex > Null.NullInteger)
                        {
                            kvp.Value[localizedIndex] = updatedTab;
                        }
                    });
                }
            }
        }

        #endregion
    }
}
