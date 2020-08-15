namespace DotNetNuke.Abstractions.Clients
{
    using DotNetNuke.Abstractions.Clients.Providers;

    public interface IClientDependencySettings
    {
        ICompositeFileProcessingProvider DefaultCompositeFileProcessingProvider { get; }
    }
}
