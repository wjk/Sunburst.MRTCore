namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceMap
{
    public uint ResourceCount
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public ResourceCandidate GetValue(string name)
    {
        throw new NotImplementedException();
    }

    public ResourceCandidate GetValue(string name, ResourceContext context)
    {
        throw new NotImplementedException();
    }

    public KeyValuePair<string, ResourceCandidate> GetValueByIndex(int index)
    {
        throw new NotImplementedException();
    }

    public KeyValuePair<string, ResourceCandidate> GetValueByIndex(int index, ResourceContext context)
    {
        throw new NotImplementedException();
    }

    public ResourceCandidate TryGetValue(string name)
    {
        throw new NotImplementedException();
    }

    public ResourceCandidate TryGetValue(string name, ResourceContext context)
    {
        throw new NotImplementedException();
    }
}
