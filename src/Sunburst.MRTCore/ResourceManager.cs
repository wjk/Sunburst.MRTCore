using System.Runtime.InteropServices;

namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceManager
{
    private IntPtr resourceManagerHandle = IntPtr.Zero;

    public ResourceManager()
    {
        int hr = NativeMethods.MrmCreateResourceManager(ResourceLoader.GetDefaultResourcePath(), out this.resourceManagerHandle);

        if (!NativeMethods.IsResourceNotFound(hr))
        {
            Marshal.ThrowExceptionForHR(hr);
        }
    }

    public ResourceManager(string fileName)
    {
        int hr = NativeMethods.MrmCreateResourceManager(fileName, out this.resourceManagerHandle);

        if (!NativeMethods.IsResourceNotFound(hr))
        {
            Marshal.ThrowExceptionForHR(hr);
        }
    }

    ~ResourceManager()
    {
        NativeMethods.MrmDestroyResourceManager(this.resourceManagerHandle);
    }

    public ResourceMap MainResourceMap => new ResourceMap(this, this.resourceManagerHandle, IntPtr.Zero);
 
    public event Action<ResourceManager, ResourceNotFoundEventArgs>? ResourceNotFound;

    public ResourceContext CreateResourceContext()
    {
        IntPtr contextHandle = IntPtr.Zero;
        if (this.resourceManagerHandle != IntPtr.Zero)
        {
            int hr = NativeMethods.MrmCreateResourceContext(this.resourceManagerHandle, out contextHandle);
            Marshal.ThrowExceptionForHR(hr);
        }

        return new ResourceContext(contextHandle);
    }

    internal ResourceCandidate? HandleResourceNotFound(ResourceContext context, string name)
    {
        ResourceNotFoundEventArgs args = new ResourceNotFoundEventArgs(context, name);
        this.ResourceNotFound?.Invoke(this, args);

        ResourceCandidate? candidate = args.ResolvedCandidate;
        if (candidate != null)
        {
            candidate.SetQualifierValuesFromContext(context);
        }

        return candidate;
    }
}
