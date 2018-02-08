namespace DotNetNuke.Web.Mvc.RazorPages.Common
{
    public interface IAntiForgery
    {
        string CookieName { get; }
        void Validate(string cookieToken, string headerToken);
    }
}