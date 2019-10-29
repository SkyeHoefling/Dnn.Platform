#region Copyright

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

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;


using DotNetNuke.Logging;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.Web.Api;

using Microsoft.Extensions.Logging;

namespace DotNetNuke.Web.InternalServices
{
    [DnnAuthorize]
    public class EventLogServiceController : DnnApiController
    {
        private readonly ILogger Logger;

        public EventLogServiceController(ILogger<EventLogServiceController> logger)
        {
            Logger = logger;
        }

        [HttpGet]
        [DnnAuthorize(StaticRoles = "Administrators")]
        public HttpResponseMessage GetLogDetails(string guid)
        {
            Guid logId;
            if (string.IsNullOrEmpty(guid) || !Guid.TryParse(guid, out logId))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var LogInformation = new LogInformation {LogGUID = guid};
                LogInformation = EventLogController.Instance.GetSingleLog(LogInformation, LoggingProvider.ReturnType.LogInformationObjects) as LogInformation;
                if (LogInformation == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                return Request.CreateResponse(HttpStatusCode.OK, new
                                                                     {
                                                                         Title = Localization.GetSafeJSString("CriticalError.Error", Localization.SharedResourceFile),
                                                                         Content = GetPropertiesText(LogInformation)
                                                                     });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        private string GetPropertiesText(LogInformation LogInformation)
        {
            var objLogProperties = LogInformation.LogProperties;
            var str = new StringBuilder();
            int i;
            for (i = 0; i <= objLogProperties.Count - 1; i++)
            {
                //display the values in the Panel child controls.
                var ldi = (LogDetailInfo)objLogProperties[i];
                if (ldi.PropertyName == "Message")
                {
                    str.Append("<p><strong>" + ldi.PropertyName + "</strong>:</br><pre>" + HttpUtility.HtmlEncode(ldi.PropertyValue) + "</pre></p>");
                }
                else
                {
                    str.Append("<p><strong>" + ldi.PropertyName + "</strong>:" + HttpUtility.HtmlEncode(ldi.PropertyValue) + "</p>");
                }
            }
            str.Append("<p><b>Server Name</b>: " + HttpUtility.HtmlEncode(LogInformation.LogServerName) + "</p>");
            return str.ToString();
        }
    }
}