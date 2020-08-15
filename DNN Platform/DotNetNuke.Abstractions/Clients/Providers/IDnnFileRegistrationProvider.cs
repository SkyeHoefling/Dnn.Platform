namespace DotNetNuke.Abstractions.Clients.Providers
{
    using System.Collections.Generic;
    
    public interface IDnnFileRegistrationProvider
    {
        /// <summary>
        /// Gets the default name of the provider
        /// </summary>
        string DefaultName { get; }

        /// <summary>
        /// Renders a single javascript file to the DOM
        /// </summary>
        /// <param name="js">The location of the javascript file.</param>
        /// <param name="htmlAttributes">Additional attributes to add to the rendered 'script' tag.</param>
        /// <returns>The final string content to render to the DOM.</returns>
        string RenderSingleJsFile(string js, IDictionary<string, string> htmlAttributes);

        /// <summary>
        /// Renders a single CSS file to the DOM
        /// </summary>
        /// <param name="js">The location of the CSS file.</param>
        /// <param name="htmlAttributes">Additional attributes to add to the rendered 'link' tag.</param>
        /// <returns>The final string content to render to the DOM.</returns>
        string RenderSingleCssFile(string css, IDictionary<string, string> htmlAttributes);

        /// <summary>
        /// Checks if the composite files option is set for the current portal (DNN site settings).
        /// If not enabled at the portal level it defers to the core CDF setting (web.config).
        /// </summary>
        bool EnableCompositeFiles { get; }
    }
}
