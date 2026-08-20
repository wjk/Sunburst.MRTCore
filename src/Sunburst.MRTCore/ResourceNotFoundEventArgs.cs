namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceNotFoundEventArgs
{
    internal ResourceNotFoundEventArgs(ResourceContext context, string name)
    {
        this.ResourceContext = context;
        this.Name = name;
    }

    public ResourceContext ResourceContext { get; init; }

    public string Name { get; init; }

    internal ResourceCandidate ResolvedCandidate { get; private set; } = null;

    public void SetResolvedCandidate(ResourceCandidate candidate)
    {
        throw new NotImplementedException();
    }
}
