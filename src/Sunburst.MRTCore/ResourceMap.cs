namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceMap
{
    private readonly ResourceManager resourceManager;
    private readonly IntPtr managerHandle;
    private readonly IntPtr mapHandle;
    private uint? resourceCount = null;

    internal ResourceMap(ResourceManager manager, IntPtr managerHandle, IntPtr mapHandle)
    {
        this.resourceManager = manager;
        this.managerHandle = manager;
        this.mapHandle = mapHandle;
    }

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

    private ResourceCandidate GetValueImpl(ResourceContext context, string resource, bool treatNotFoundAsOK)
    {
        throw new NotImplementedException();
    }

    private KeyValuePair<string, ResourceCandidate> GetValueByIndexImpl(ResourceCandidate context, uint index)
    {
        throw new NotImplementedException();
    }
}
