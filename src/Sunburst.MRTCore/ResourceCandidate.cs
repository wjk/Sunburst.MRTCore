using System.Runtime.InteropServices;

namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceCandidate
{
    private string? stringData;
    private byte[] blobData;
    private ResourceCandidateKind kind = ResourceCandidateKind.Unknown;
    private IntPtr resourceManagerHandle = IntPtr.Zero;
    private IntPtr resourceContextHandle = IntPtr.Zero;
    private IntPtr resourceMapHandle = IntPtr.Zero;
    private uint? resourceIndex = null;
    private string? resourceId = null;
    private Dictionary<string, string>? qualifierValueMap = null;

    public ResourceCandidate(ResourceCandidateKind kind, string data)
    {
        this.stringData = data;
        this.blobData = Array.Empty<byte>();
        this.kind = kind;

        if (this.kind != ResourceCandidateKind.String && this.kind != ResourceCandidateKind.FilePath)
        {
            throw new ArgumentException("Resource type mismatch");
        }
    }

    public ResourceCandidate(byte[] data)
    {
        this.stringData = null;
        this.blobData = data;
        this.kind = ResourceCandidateKind.EmbeddedData;
    }

    internal ResourceCandidate(IntPtr managerHandle, IntPtr contextHandle, IntPtr mapHandle, uint index, string id, ResourceCandidateKind kind, string data)
    {
        this.resourceManagerHandle = managerHandle;
        this.resourceContextHandle = contextHandle;
        this.resourceMapHandle = mapHandle;
        this.resourceIndex = index;
        this.resourceId = id;
        this.kind = kind;
        this.stringData = data;
        this.blobData = Array.Empty<byte>();
    }

    internal ResourceCandidate(IntPtr managerHandle, IntPtr contextHandle, IntPtr mapHandle, uint index, string id, ResourceCandidateKind kind, byte[] data)
    {
        this.resourceManagerHandle = managerHandle;
        this.resourceContextHandle = contextHandle;
        this.resourceMapHandle = mapHandle;
        this.resourceIndex = index;
        this.resourceId = id;
        this.kind = ResourceCandidateKind.EmbeddedData;
        this.stringData = null;
        this.blobData = Array.Empty<byte>();
    }

    public string ValueAsString
    {
        get
        {
            if (this.kind == ResourceCandidateKind.String || this.kind == ResourceCandidateKind.FilePath)
            {
                if (this.stringData == null)
                {
                    throw new InvalidOperationException("Resource type set to string (or file path) but string data not set");
                }

                return this.stringData;
            }
            else
            {
                throw new ArgumentException("Resource type mismatch");
            }
        }
    }

    public byte[] ValueAsBytes
    {
        get
        {
            if (this.kind == ResourceCandidateKind.EmbeddedData)
            {
                return this.blobData;
            }
            else
            {
                throw new ArgumentException("Resource type mismatch");
            }
        }
    }

    public ResourceCandidateKind Kind => this.kind;

    public IReadOnlyDictionary<string, string> QualifierValues
    {
        get
        {
            if (this.qualifierValueMap == null)
            {
                ResourceCandidateKind kind;
                IntPtr data, stringValue, qualifierNames, qualifierValues;
                uint qualifierCount;

                if (this.resourceIndex == null || !this.resourceIndex.HasValue)
                {
                    if (this.resourceId == null)
                    {
                        throw new InvalidOperationException("Resource ID not set");
                    }

                    int hr = NativeMethods.MrmLoadStringOrEmbeddedResourceWithQualifierValues(
                        this.resourceManagerHandle, this.resourceContextHandle, this.resourceMapHandle,
                        this.resourceId, out kind, out stringValue, out data, out qualifierCount,
                        out qualifierNames, out qualifierValues);
                    Marshal.ThrowExceptionForHR(hr);
                }
                else
                {
                    if (this.resourceIndex == null || !this.resourceIndex.HasValue)
                    {
                        throw new InvalidOperationException("Resource index not set");
                    }

                    int hr = NativeMethods.MrmLoadStringOrEmbeddedResourceByIndexWithQualifierValues(
                        this.resourceManagerHandle, this.resourceContextHandle, this.resourceMapHandle,
                        this.resourceIndex.Value, out kind, out stringValue, out data, out qualifierCount,
                        out qualifierNames, out qualifierValues);
                    Marshal.ThrowExceptionForHR(hr);
                }

                this.qualifierValueMap = new Dictionary<string, string>();
                for (uint i = 0; i < qualifierCount; i++)
                {
                    string? key = Marshal.PtrToStringUni(Marshal.ReadIntPtr(qualifierNames, (int)(i * IntPtr.Size)));
                    string? value = Marshal.PtrToStringUni(Marshal.ReadIntPtr(qualifierValues, (int)(i * IntPtr.Size)));

                    if (key != null && value != null)
                    {
                        this.qualifierValueMap[key] = value;
                    }
                }

                NativeMethods.MrmFreeResource(stringValue);
                NativeMethods.MrmFreeResource(qualifierNames);
                NativeMethods.MrmFreeResource(qualifierValues);
                NativeMethods.MrmFreeResource(data);
            }

            return this.qualifierValueMap;
        }
    }

    internal void SetQualifierValuesFromContext(ResourceContext context)
    {
        if (this.qualifierValueMap == null)
        {
            this.qualifierValueMap = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in context.QualifierValues)
            {
                this.qualifierValueMap[pair.Key] = pair.Value;
            }
        }
    }
}
