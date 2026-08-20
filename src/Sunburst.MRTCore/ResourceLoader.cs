namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceLoader
{
    private IntPtr resourceManagerHandle = IntPtr.Zero;
    private IntPtr resourceMapHandle = IntPtr.Zero;

    public ResourceLoader()
    {
        throw new NotImplementedException();
    }

    public ResourceLoader(string filename)
    {
        throw new NotImplementedException();
    }

    public ResourceLoader(string filename, string resourceMap)
    {
        throw new NotImplementedException();
    }

    ~ResourceLoader()
    {
        throw new NotImplementedException();
    }

    public static string GetDefaultResourcePath()
    {
        throw new NotImplementedException();
    }

    public string GetString(string resourceId)
    {
        throw new NotImplementedException();
    }

    public string GetStringForUri(Uri uri)
    {
        throw new NotImplementedException();
    }
}
