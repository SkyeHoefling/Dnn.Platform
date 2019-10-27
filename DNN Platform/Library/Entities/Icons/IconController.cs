#region Copyright
// 
// DotNetNuke® - https://www.dnnsoftware.com
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
using System.IO;
using System.Web.Hosting;

using DotNetNuke.Collections.Internal;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#endregion

namespace DotNetNuke.Entities.Icons
{
    /// <summary>
    /// IconController provides all operation to icons.
    /// </summary>
    /// <remarks>
    /// Tab is equal to page in DotNetNuke.
    /// Tabs will be a sitemap for a poatal, and every request at first need to check whether there is valid tab information
    /// include in the url, if not it will use default tab to display information.
    /// </remarks>
    public class IconController
    {
    	private static readonly ILogger Logger = Globals.DependencyProvider.GetService<ILoggerFactory>().CreateLogger(typeof (IconController));
        #region Constants

        public const string DefaultIconSize = "16X16";
        public const string DefaultLargeIconSize = "32X32";
        public const string DefaultIconStyle = "Standard";
        public const string IconKeyName = "IconKey";
        public const string IconSizeName = "IconSize";
        public const string IconStyleName = "IconStyle";

        #endregion


        /// <summary>
        /// Gets the Icon URL.
        /// </summary>
        /// <param name="key">Key to icon, e.g. edit</param>        
        /// <returns>Link to the image, e.g. /Icons/Sigma/edit_16x16_standard.png</returns>
        public static string IconURL(string key)
        {
            return IconURL(key, DefaultIconSize, DefaultIconStyle);
        }

        /// <summary>
        /// Gets the Icon URL.
        /// </summary>
        /// <param name="key">Key to icon, e.g. edit</param>        
        /// <param name="size">Size of icon, e.g.16x16 (default) or 32x32</param>
        /// <returns>Link to the image, e.g. /Icons/Sigma/edit_16x16_standard.png</returns>
        public static string IconURL(string key, string size)
        {
            return IconURL(key, size, DefaultIconStyle);
        }

        /// <summary>
        /// Gets the Icon URL.
        /// </summary>
        /// <param name="key">Key to icon, e.g. edit</param>        
        /// <param name="size">Size of icon, e.g.16x16 (default) or 32x32</param>
        /// <param name="style">Style of icon, e.g. Standard (default)</param>
        /// <returns>Link to the image, e.g. /Icons/Sigma/edit_16x16_standard.png</returns>
        public static string IconURL(string key, string size, string style)
        {
            if (string.IsNullOrEmpty(key)) 
                return string.Empty;

            if (string.IsNullOrEmpty(size)) 
                size = DefaultIconSize;

            if (string.IsNullOrEmpty(style))
                style = DefaultIconStyle;

            string fileName = string.Format("{0}/{1}_{2}_{3}.png", PortalSettings.Current.DefaultIconLocation, key, size, style);

            //In debug mode, we want to warn (onluy once) if icon is not present on disk
#if DEBUG
            CheckIconOnDisk(fileName);
#endif
            return Globals.ApplicationPath + "/" + fileName;
        }

        public static string GetFileIconUrl(string extension)
        {
            if (!string.IsNullOrEmpty(extension) && File.Exists(HostingEnvironment.MapPath(IconURL("Ext" + extension, "32x32", "Standard"))))
            {
                return IconURL("Ext" + extension, "32x32", "Standard");
            }
            return IconURL("ExtFile", "32x32", "Standard");
        }

        private static readonly SharedDictionary<string, bool> _iconsStatusOnDisk = new SharedDictionary<string, bool>();
        private static void CheckIconOnDisk(string path)
        {            
            using (_iconsStatusOnDisk.GetReadLock())
            {                
                if (_iconsStatusOnDisk.ContainsKey(path)) 
                    return;
            }

            using (_iconsStatusOnDisk.GetWriteLock())
            {
                if (!_iconsStatusOnDisk.ContainsKey(path))
                {
                    _iconsStatusOnDisk.Add(path, true);
                    string iconPhysicalPath = Path.Combine(Globals.ApplicationMapPath, path.Replace('/', '\\'));
                    if (!File.Exists(iconPhysicalPath)) 
						Logger.LogWarning(string.Format("Icon Not Present on Disk {0}", iconPhysicalPath));
                }
            }            
        }

        public static string[] GetIconSets()
        {
            string iconPhysicalPath = Path.Combine(Globals.ApplicationMapPath, "icons");
            var iconRootDir = new DirectoryInfo(iconPhysicalPath);
            string result = "";
            foreach (var iconDir in iconRootDir.EnumerateDirectories())
            {
                string testFile = Path.Combine(iconDir.FullName,"About_16x16_Standard.png");
                if (File.Exists(testFile))
                    result += iconDir.Name + ",";
            }
            return result.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}