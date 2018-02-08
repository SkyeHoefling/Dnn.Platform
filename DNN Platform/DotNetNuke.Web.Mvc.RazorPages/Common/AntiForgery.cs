using System;
using DotNetNuke.Framework;

namespace DotNetNuke.Web.Mvc.RazorPages.Common
{
    public class AntiForgery : ServiceLocator<IAntiForgery, AntiForgery>
    {
        protected override Func<IAntiForgery> GetFactory()
        {
            return () => new AntiForgeryImpl();
        }
    }
}