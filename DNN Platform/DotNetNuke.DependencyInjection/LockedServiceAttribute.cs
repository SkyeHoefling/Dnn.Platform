using System;

namespace DotNetNuke.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class LockedServiceAttribute : Attribute
    {
    }
}
