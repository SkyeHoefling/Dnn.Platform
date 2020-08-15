namespace DotNetNuke.Abstractions
{
    using DotNetNuke.Abstractions.Clients;

    public interface IDnnPage
    {
        IDnnServer Server { get; }
        void AddInclude(string name, IDnnInclude include);
    }   
}
