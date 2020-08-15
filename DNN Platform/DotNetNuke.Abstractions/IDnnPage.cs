using DotNetNuke.Abstractions.Clients.ClientResourceManagement;
using System;

namespace DotNetNuke.Abstractions
{
    public interface IDnnPage
    {
        IDnnServer Server { get; }
        void AddInclude(string name, IDnnInclude callback);
    }   
}
