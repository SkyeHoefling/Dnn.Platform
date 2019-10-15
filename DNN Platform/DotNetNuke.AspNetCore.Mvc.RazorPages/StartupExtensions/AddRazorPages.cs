using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.PlatformAbstractions;
using System.Diagnostics;
using System.IO;

namespace DotNetNuke.AspNetCore.Mvc.RazorPages.StartupExtensions
{
    internal static class StartupExtensions_AddRazorPages
    {
        internal static void AddRazorPages(this IServiceCollection services)
        {
            services.AddSingleton(PlatformServices.Default.Application);
            services.AddSingleton<IHostingEnvironment>(BuildHostingEnvironment());
            services.Configure<RazorViewEngineOptions>(BuildViewEngineOptions);
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<DiagnosticSource>(new DiagnosticListener("Microsoft.AspNetCore"));

            services.AddLogging();
            services.AddMvc();

            IFileProvider BuildFileProvider()
            {
                return new PhysicalFileProvider(Directory.GetCurrentDirectory());
            }

            HostingEnvironment BuildHostingEnvironment()
            {
                var appDirectory = Directory.GetCurrentDirectory();
                return new HostingEnvironment
                {
                    WebRootFileProvider = BuildFileProvider(),
                    ApplicationName = "DotNetNuke.AspNetCore.Mvc.RazorPages"
                };
            }

            void BuildViewEngineOptions(RazorViewEngineOptions options)
            {
                options.FileProviders.Clear();
                options.FileProviders.Add(BuildFileProvider());
            }
        }
    }
}
