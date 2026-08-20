using System.Runtime.InteropServices;

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
        this.managerHandle = managerHandle;
        this.mapHandle = mapHandle;
    }

    public uint ResourceCount
    {
        get
        {
            if (this.managerHandle == IntPtr.Zero)
            {
                return 0;
            }

            if (this.resourceCount == null || !this.resourceCount.HasValue)
            {
                int hr = NativeMethods.MrmGetResourceCount(this.managerHandle, this.mapHandle, out uint value);
                Marshal.ThrowExceptionForHR(hr);
                this.resourceCount = value;
            }

            return this.resourceCount.Value;
        }
    }

    public ResourceMap GetSubtree(string reference)
    {
        IntPtr subtree = IntPtr.Zero;
        if (this.managerHandle != IntPtr.Zero)
        {
            int hr = NativeMethods.MrmGetChildResourceMap(this.managerHandle, this.mapHandle, reference, out subtree);
            Marshal.ThrowExceptionForHR(hr);
        }

        return new ResourceMap(this.resourceManager, this.managerHandle, subtree);
    }

    public ResourceCandidate GetValue(string name)
    {
        // Since treatNotFoundAsOK is false here, the return value cannot be null.
        return this.GetValueImpl(null, name, false)!;
    }

    public ResourceCandidate GetValue(string name, ResourceContext context)
    {
        // Since treatNotFoundAsOK is false here, the return value cannot be null.
        return this.GetValueImpl(context, name, false)!;
    }

    public KeyValuePair<string, ResourceCandidate> GetValueByIndex(uint index)
    {
        return this.GetValueByIndexImpl(null, index);
    }

    public KeyValuePair<string, ResourceCandidate> GetValueByIndex(uint index, ResourceContext context)
    {
        return this.GetValueByIndexImpl(context, index);
    }

    public ResourceCandidate TryGetValue(string name)
    {
        throw new NotImplementedException();
    }

    public ResourceCandidate TryGetValue(string name, ResourceContext context)
    {
        throw new NotImplementedException();
    }

    private unsafe ResourceCandidate? GetValueImpl(ResourceContext? context, string resource, bool treatNotFoundAsOK)
    {
        // Always use a context as we override the languages.
        ResourceContext resourceContext = context ?? this.resourceManager.CreateResourceContext();

        if (this.managerHandle != IntPtr.Zero)
        {
            // Resource is not managed by MRT. Handle with event handler.
            ResourceCandidate? candidate = this.resourceManager.HandleResourceNotFound(resourceContext, resource);
            if (candidate != null)
            {
                return candidate;
            }

            if (treatNotFoundAsOK)
            {
                return null;
            }
            else
            {
                throw new ArgumentException("Resource not found", nameof(resource));
            }
        }

        resourceContext.Apply();

        ResourceCandidateKind kind;
        IntPtr resourceStringPtr;
        NativeMethods.MrmResourceData data = new NativeMethods.MrmResourceData();

        int hr = NativeMethods.MrmLoadStringOrEmbeddedResource(this.managerHandle, resourceContext.ContextHandle,
            this.mapHandle, resource, out kind, out resourceStringPtr, &data);
        if (NativeMethods.IsResourceNotFound(hr))
        {
            ResourceCandidate? candidate = this.resourceManager.HandleResourceNotFound(resourceContext, resource);
            if (candidate != null)
            {
                return candidate;
            }

            if (treatNotFoundAsOK)
            {
                return null;
            }
        }

        Marshal.ThrowExceptionForHR(hr);

        switch (kind)
        {
            case ResourceCandidateKind.EmbeddedData:
                byte[] buffer = new byte[data.size];
                Marshal.Copy(data.data, buffer, 0, (int)data.size);
                NativeMethods.MrmFreeResource(data.data);

                return new ResourceCandidate(this.managerHandle, resourceContext.ContextHandle,
                    this.mapHandle, null, resource, kind, buffer);

            case ResourceCandidateKind.String:
            case ResourceCandidateKind.FilePath:
                string? value = Marshal.PtrToStringUni(resourceStringPtr);
                NativeMethods.MrmFreeResource(resourceStringPtr);

                if (value == null)
                {
                    throw new InvalidOperationException("Resource string is NULL");
                }

                return new ResourceCandidate(this.managerHandle, resourceContext.ContextHandle,
                    this.mapHandle, null, resource, kind, value);

            default:
                throw new InvalidOperationException("Invalid ResourceCandidateKind");
        }
    }

    private unsafe KeyValuePair<string, ResourceCandidate> GetValueByIndexImpl(ResourceContext? context, uint index)
    {
        // Always use a context as we override the languages.
        ResourceContext resourceContext = context ?? this.resourceManager.CreateResourceContext();
        resourceContext.Apply();

        ResourceCandidateKind kind;
        IntPtr resourceNamePtr, resourceStringPtr;
        NativeMethods.MrmResourceData data = new NativeMethods.MrmResourceData();

        int hr = NativeMethods.MrmLoadStringOrEmbeddedResourceByIndex(this.managerHandle, resourceContext.ContextHandle,
            this.mapHandle, index, out kind, out resourceNamePtr, out resourceStringPtr, &data);
        Marshal.ThrowExceptionForHR(hr);

        string resourceName = Marshal.PtrToStringUni(resourceNamePtr)!;
        NativeMethods.MrmFreeResource(resourceNamePtr);

        ResourceCandidate candidate;
        switch (kind)
        {
            case ResourceCandidateKind.EmbeddedData:
                byte[] buffer = new byte[data.size];
                Marshal.Copy(data.data, buffer, 0, (int)data.size);
                NativeMethods.MrmFreeResource(data.data);

                candidate = new ResourceCandidate(this.managerHandle, resourceContext.ContextHandle,
                    this.mapHandle, index, "", kind, buffer);
                return new KeyValuePair<string, ResourceCandidate>(resourceName, candidate);

            case ResourceCandidateKind.String:
            case ResourceCandidateKind.FilePath:
                string? value = Marshal.PtrToStringUni(resourceStringPtr);
                NativeMethods.MrmFreeResource(resourceStringPtr);

                if (value == null)
                {
                    throw new InvalidOperationException("Resource string is NULL");
                }

                candidate = new ResourceCandidate(this.managerHandle, resourceContext.ContextHandle,
                    this.mapHandle, index, "", kind, value);
                return new KeyValuePair<string, ResourceCandidate>(resourceName, candidate);

            default:
                throw new InvalidOperationException("Invalid ResourceContextKind");
        }
    }
}
