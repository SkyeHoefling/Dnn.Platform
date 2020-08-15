namespace DotNetNuke.Web.Client
{
    using DotNetNuke.Abstractions.Clients;
    using DotNetNuke.Abstractions.Clients.Providers;
    using DotNetNuke.Instrumentation;

    public class ClientDependencySettings : IClientDependencySettings
    {
        private static readonly ILog Logger = LoggerSource.Instance.GetLogger(typeof(ClientDependencySettings));

        readonly ClientDependency.Core.Config.ClientDependencySettings _settings;
        public ClientDependencySettings()
        {
            _settings = ClientDependency.Core.Config.ClientDependencySettings.Instance;
        }

        ICompositeFileProcessingProvider IClientDependencySettings.DefaultCompositeFileProcessingProvider
        {
            get
            {
                var provider = _settings.DefaultCompositeFileProcessingProvider;
                if (provider is ICompositeFileProcessingProvider dnnProvider)
                    return dnnProvider;

                Logger.Error($"Unable to cast '{provider.GetType().FullName}' to type of '{typeof(ICompositeFileProcessingProvider).FullName}'");
                return null;
            }
        }
    }
}
