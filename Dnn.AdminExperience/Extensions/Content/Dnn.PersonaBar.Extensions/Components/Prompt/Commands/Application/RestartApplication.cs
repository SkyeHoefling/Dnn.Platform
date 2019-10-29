using System;
using Dnn.PersonaBar.Library.Prompt;
using Dnn.PersonaBar.Library.Prompt.Attributes;
using Dnn.PersonaBar.Library.Prompt.Models;
using DotNetNuke.Common;
using DotNetNuke.Logging;
using DotNetNuke.Services.Log.EventLog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Dnn.PersonaBar.Prompt.Components.Commands.Application
{
    [ConsoleCommand("restart-application", Constants.HostCategory, "Prompt_RestartApplication_Description")]
    public class RestartApplication : ConsoleCommandBase
    {
        private static readonly ILogger Logger = Globals.DependencyProvider.GetService<ILogger<RestartApplication>>();

        public override ConsoleResultModel Run()
        {
            try
            {
                var log = new LogInformation
                {
                    BypassBuffering = true,
                    LogTypeKey = EventLogController.EventLogType.HOST_ALERT.ToString()
                };
                log.AddProperty("Message", LocalizeString("Prompt_UserRestart"));
                LogController.Instance.AddLog(log);
                DotNetNuke.Common.Utilities.Config.Touch();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return new ConsoleErrorResultModel(LocalizeString("Prompt_UserRestart_Error"));
            }
            return new ConsoleResultModel(LocalizeString("Prompt_UserRestart_Success")) { MustReload = true };
        }

        public override string LocalResourceFile => Constants.LocalResourcesFile;
    }
}