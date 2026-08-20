namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceCandidate
{
    private string stringData;
    private byte[] blobData;
    private ResourceCandidateKind kind = ResourceCandidateKind.Unknown;
    private IntPtr resourceManagerHandle = IntPtr.Zero;
    private IntPtr resourceContextHandle = IntPtr.Zero;
    private IntPtr resourceMapHandle = IntPtr.Zero;
    private uint? resourceIndex = null;
    private string resourceId;

    public ResourceCandidate(ResourceCandidateKind kind, string data)
    {
        throw new NotImplementedException();
    }

    public ResourceCandidate(byte[] data)
    {
        throw new NotImplementedException();
    }

    internal ResourceCandidate(IntPtr managerHandle, IntPtr contextHandle, IntPtr mapHandle, uint index, string id, ResourceCandidateKind kind, string data)
    {
        throw new NotImplementedException();
    }

    internal ResourceCandidate(IntPtr managerHandle, IntPtr contextHandle, IntPtr mapHandle, uint index, string id, ResourceCandidateKind kind, byte[] data)
    {
        throw new NotImplementedException();
    }

    public string ValueAsString
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public byte[] ValueAsBytes
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public ResourceCandidateKind Kind
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public IReadOnlyDictionary<string, string> QualifierValues
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    internal void SetQualifierValuesFromContext(ResourceContext context)
    {
        throw new NotImplementedException();
    }
}
