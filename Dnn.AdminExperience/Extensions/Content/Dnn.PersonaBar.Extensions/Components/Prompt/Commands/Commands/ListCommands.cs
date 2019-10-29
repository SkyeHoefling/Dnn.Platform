using System;
using System.Linq;
using Dnn.PersonaBar.Library.Prompt;
using Dnn.PersonaBar.Library.Prompt.Attributes;
using Dnn.PersonaBar.Library.Prompt.Models;
using Dnn.PersonaBar.Prompt.Components.Repositories;
using DotNetNuke.Common;
using DotNetNuke.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Dnn.PersonaBar.Prompt.Components.Commands.Commands
{
    [ConsoleCommand("list-commands", Constants.GeneralCategory, "Prompt_ListCommands_Description")]
    public class ListCommands : ConsoleCommandBase
    {
        private static readonly ILogger Logger = Globals.DependencyProvider.GetService<ILogger<ListCommands>>();
        public override string LocalResourceFile => Constants.LocalResourcesFile;

        public override ConsoleResultModel Run()
        {

            try
            {
                var lstOut = CommandRepository.Instance.GetCommands().Values.OrderBy(c => c.Name + '.' + c.Name).ToList();
                return new ConsoleResultModel(string.Format(LocalizeString("Prompt_ListCommands_Found"), lstOut.Count))
                {
                    Records = lstOut.Count,
                    Data = lstOut,
                    FieldOrder = new[] {
                    "Name", "Description", "Version", "Category" }
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return new ConsoleErrorResultModel(LocalizeString("Prompt_ListCommands_Error"));
            }
        }
    }
}