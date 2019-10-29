using System;
using Dnn.PersonaBar.Library.Prompt;
using Dnn.PersonaBar.Library.Prompt.Attributes;
using Dnn.PersonaBar.Library.Prompt.Models;
using DotNetNuke.Common;
using DotNetNuke.Logging;
using DotNetNuke.Services.Log.EventLog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Dnn.PersonaBar.Prompt.Components.Commands.Portal
{
    [ConsoleCommand("clear-log", Constants.PortalCategory, "Prompt_ClearLog_Description")]
    public class ClearLog : ConsoleCommandBase
    {
        public override string LocalResourceFile => Constants.LocalResourcesFile;

        private static readonly ILogger Logger = Globals.DependencyProvider.GetService<ILogger<ClearLog>>();
        public override ConsoleResultModel Run()
        {
            try
            {
                EventLogController.Instance.ClearLog();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return new ConsoleErrorResultModel(LocalizeString("Prompt_ClearLog_Error"));
            }
            return new ConsoleResultModel(LocalizeString("Prompt_ClearLog_Success"));

        }
    }
}
