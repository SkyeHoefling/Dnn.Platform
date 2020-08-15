namespace DotNetNuke.Framework
{
    using DotNetNuke.Abstractions;
    using System.Web;

    internal class DnnServer : IDnnServer
    {
        readonly HttpServerUtility _server;
        public DnnServer(HttpServerUtility server)
        {
            _server = server;
        }

        public string MapPath(string path) => _server.MapPath(path);
    }
}
