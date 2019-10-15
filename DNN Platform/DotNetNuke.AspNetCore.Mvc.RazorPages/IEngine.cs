using DotNetNuke.UI.Modules;
using System.IO;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages
{
    public interface IEngine
    {
        // TODO - this should be moved to some shared lib

        /// <summary>
        /// Renders the specified module engine and writes the
        /// output HTML to the <see cref="TextWriter"/>
        /// </summary>
        /// <param name="file">The script file to render</param>
        /// <param name="moduleContext">The current module context</param>
        /// <param name="localResourceFile">The local resources for any strings</param>
        /// <param name="writer">The <see cref="TextWriter"/> that prints the in final output HTML text</param>
        void Render(string file, ModuleInstanceContext moduleContext, string localResourceFile, TextWriter writer);
    }
}
