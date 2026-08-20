namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceManager
{
    private IntPtr resourceManagerHandle = IntPtr.Zero;
    private Mutex mutex;

    public ResourceManager()
    {
        throw new NotImplementedException();
    }

    public ResourceManager(string fileName)
    {
        throw new NotImplementedException();
    }

    ~ResourceManager()
    {
        throw new NotImplementedException();
    }

    public ResourceMap MainResourceMap
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public event Action<ResourceManager, ResourceNotFoundEventArgs> ResourceNotFound;

    public ResourceContext CreateResourceContext()
    {
        throw new NotImplementedException();
    }

    private ResourceCandidate HandleResourceNotFound(ResourceContext context, string name)
    {
        throw new NotImplementedException();
    }
}
