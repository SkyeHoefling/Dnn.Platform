namespace DotNetNuke.Abstractions.Clients.ClientResourceManagement
{
    public interface IClientDependencySettings
    {
        ICompositeFileProcessingProvider DefaultCompositeFileProcessingProvider { get; }
    }
}
