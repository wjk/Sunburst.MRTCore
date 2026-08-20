namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceNotFoundEventArgs
{
    public ResourceContext ResourceContext
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public string Name
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public void SetResolvedCandidate(ResourceCandidate candidate)
    {
        throw new NotImplementedException();
    }
}
