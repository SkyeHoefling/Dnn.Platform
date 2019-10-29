using System;
using Dnn.PersonaBar.Library.Prompt;
using Dnn.PersonaBar.Library.Prompt.Attributes;
using Dnn.PersonaBar.Library.Prompt.Models;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Dnn.PersonaBar.Prompt.Components.Commands.Host
{
    [ConsoleCommand("clear-cache", Constants.HostCategory, "Prompt_ClearCache_Description")]
    public class ClearCache : ConsoleCommandBase
    {
        public override string LocalResourceFile => Constants.LocalResourcesFile;

        private static readonly ILogger Logger = Globals.DependencyProvider.GetService<ILogger<ClearCache>>();

        public override ConsoleResultModel Run()
        {
            try
            {
                DataCache.ClearCache();
                DotNetNuke.Web.Client.ClientResourceManagement.ClientResourceManager.ClearCache();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return new ConsoleErrorResultModel(LocalizeString("Prompt_ClearCache_Error"));
            }
            return new ConsoleResultModel(LocalizeString("Prompt_ClearCache_Success")) { MustReload = true };
        }
    }
}
