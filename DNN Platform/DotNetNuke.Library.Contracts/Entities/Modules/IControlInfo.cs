namespace DotNetNuke.Library.Contracts.Entities.Modules
{
    public interface IControlInfo
    {
        string ControlKey { get; set; }
        string ControlSrc { get; set; }
        bool SupportsPartialRendering { get; set; }
    }
}
