namespace DotNetNuke.Abstractions.Clients
{
    /// <summary>
    /// Provides the ability to request that client resources (JavaScript and CSS) be loaded on the client browser.
    /// </summary>
    public interface IClientResourceManager
    {
        /// <summary>
        /// Adds the neccessary configuration to website root web.config to use the Client Depenedecny componenet.
        /// </summary>
        void AddConfiguration();

        /// <summary>
        /// Needs documentation.
        /// </summary>
        /// <returns></returns>
        bool IsInstalled();

        /// <summary>
        /// Needs documentation.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="filePath"></param>
        void RegisterAdminStylesheet(IDnnPage page, string filePath);

        /// <summary>
        /// Needs documentation.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="filePath"></param>
        void RegisterDefaultStylesheet(IDnnPage page, string filePath);

        /// <summary>
        /// Needs documentation.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="filePath"></param>
        void RegisterFeatureStylesheet(IDnnPage page, string filePath);

        /// <summary>
        /// Needs documentation.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="filePath"></param>
        void RegisterIEStylesheet(IDnnPage page, string filePath);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        void RegisterScript(IDnnPage page, string filePath);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        void RegisterScript(IDnnPage page, string filePath, int priority);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        void RegisterScript(IDnnPage page, string filePath, FileOrder.Js priority);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        /// <param name="provider">The name of the provider responsible for rendering the script output.</param>
        void RegisterScript(IDnnPage page, string filePath, FileOrder.Js priority, string provider);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        /// <param name="provider">The name of the provider responsible for rendering the script output.</param>
        void RegisterScript(IDnnPage page, string filePath, int priority, string provider);

        /// <summary>
        /// Requests that a JavaScript file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the JavaScript resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        /// <param name="provider">The name of the provider responsible for rendering the script output.</param>
        /// <param name="name">Name of framework like Bootstrap, Angular, etc</param>
        /// <param name="version">Version nr of framework</param>
        void RegisterScript(IDnnPage page, string filePath, int priority, string provider, string name, string version);

        /// <summary>
        /// Requests that a CSS file be registered on the client browser
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the CSS resource.</param>
        void RegisterStyleSheet(IDnnPage page, string filePath);

        /// <summary>
        /// Requests that a CSS file be registered on the client browser. Defaults to rendering in the page header.
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the CSS resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        void RegisterStyleSheet(IDnnPage page, string filePath, int priority);

        /// <summary>
        /// Requests that a CSS file be registered on the client browser. Defaults to rendering in the page header.
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the CSS resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        void RegisterStyleSheet(IDnnPage page, string filePath, FileOrder.Css priority);

        /// <summary>
        /// Requests that a CSS file be registered on the client browser. Allows for overriding the default provider.
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the CSS resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        /// <param name="provider">The provider name to be used to render the css file on the page.</param>
        void RegisterStyleSheet(IDnnPage page, string filePath, int priority, string provider);

        /// <summary>
        /// Requests that a CSS file be registered on the client browser. Allows for overriding the default provider.
        /// </summary>
        /// <param name="page">The current page. Used to get a reference to the client resource loader.</param>
        /// <param name="filePath">The relative file path to the CSS resource.</param>
        /// <param name="priority">The relative priority in which the file should be loaded.</param>
        /// <param name="provider">The provider name to be used to render the css file on the page.</param>
        /// <param name="name">Name of framework like Bootstrap, Angular, etc</param>
        /// <param name="version">Version nr of framework</param>
        void RegisterStyleSheet(IDnnPage page, string filePath, int priority, string provider, string name, string version);

        /// <summary>
        /// Clear the default compisite files so that it can be generated next time.
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Needs documentation
        /// </summary>
        void ClearFileExistsCache(string path);

        /// <summary>
        /// Needs documentation
        /// </summary>
        void EnableAsyncPostBackHandler();
    }
}
