using System;

namespace DotNetNuke.Contracts
{
    public interface IModuleInfo
    {
        IControlInfo ModuleControlInfo { get; }
    }
}
