namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceCandidate
{
    public ResourceCandidate(ResourceCandidateKind kind, string data)
    {
        throw new NotImplementedException();
    }

    public ResourceCandidate(byte[] data)
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
}
