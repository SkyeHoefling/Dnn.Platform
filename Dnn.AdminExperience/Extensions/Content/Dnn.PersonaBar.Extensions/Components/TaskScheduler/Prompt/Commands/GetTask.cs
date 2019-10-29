using System;
using System.Collections.Generic;
using Dnn.PersonaBar.Library.Prompt;
using Dnn.PersonaBar.Library.Prompt.Attributes;
using Dnn.PersonaBar.Library.Prompt.Models;
using Dnn.PersonaBar.TaskScheduler.Components.Prompt.Models;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Logging;
using DotNetNuke.Services.Scheduling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dnn.PersonaBar.TaskScheduler.Components.Prompt.Commands
{
    [ConsoleCommand("get-task", Constants.SchedulerCategory, "Prompt_GetTask_Description")]
    public class GetTask : ConsoleCommandBase
    {
        private static readonly ILogger Logger = Globals.DependencyProvider.GetService<ILogger<GetTask>>();

        [FlagParameter("id", "Prompt_GetTask_FlagId", "Integer", true)]
        private const string FlagId = "id";

        private int TaskId { get; set; }

        public override void Init(string[] args, PortalSettings portalSettings, UserInfo userInfo, int activeTabId)
        {
            
            TaskId = GetFlagValue(FlagId, "Task Id", -1, true, true, true);
        }

        public override ConsoleResultModel Run()
        {
            try
            {
                var task = SchedulingProvider.Instance().GetSchedule(TaskId);
                if (task == null)
                    return new ConsoleErrorResultModel(string.Format(LocalizeString("Prompt_TaskNotFound"), TaskId));
                var tasks = new List<TaskModel> { new TaskModel(task) };
                return new ConsoleResultModel { Data = tasks, Records = tasks.Count, Output = string.Format(LocalizeString("Prompt_TaskFound"), TaskId) };
            }
            catch (Exception exc)
            {
                Logger.LogError(exc);
                return new ConsoleErrorResultModel(LocalizeString("Prompt_FetchTaskFailed"));
            }
        }
        public override string LocalResourceFile => Constants.LocalResourcesFile;
    }
}