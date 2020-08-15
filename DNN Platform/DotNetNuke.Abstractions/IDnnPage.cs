using DotNetNuke.Abstractions.Clients.ClientResourceManagement;

namespace DotNetNuke.Abstractions
{
    public interface IDnnPage
    {
        IDnnServer Server { get; }
        void AddInclude(string name, IDnnInclude include);
    }   
}
