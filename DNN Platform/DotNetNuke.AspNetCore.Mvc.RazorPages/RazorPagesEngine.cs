using DotNetNuke.UI.Modules;
using System;
using System.IO;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages
{
    internal class RazorPagesEngine : IEngine
    {
        public void Render(string file, ModuleInstanceContext moduleContext, string localResourceFile, TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
