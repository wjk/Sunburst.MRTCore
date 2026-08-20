namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceContext
{
    private readonly IntPtr resourceContextHandle;
    private byte[] qualifierNames = Array.Empty<byte>();
    private Dictionary<string, string>? qualifierValueMap = null;

    internal ResourceContext(IntPtr handle)
    {
        this.resourceContextHandle = handle;
    }

    ~ResourceContext()
    {
        NativeMethods.MrmDestroyResourceContext(this.resourceContextHandle);
    }

    public IDictionary<string, string> QualifierValues
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    internal void Apply()
    {
        throw new NotImplementedException();
    }

    private void InitializeQualifierNames()
    {
        throw new NotImplementedException();
    }

    private void InitializeQualifierValueMap()
    {
        throw new NotImplementedException();
    }

    private string GetLanguageContext()
    {
        throw new NotImplementedException();
    }
}
