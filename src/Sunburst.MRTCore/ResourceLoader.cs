using System.Runtime.InteropServices;

namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceLoader
{
    private IntPtr resourceManagerHandle = IntPtr.Zero;
    private IntPtr resourceMapHandle = IntPtr.Zero;

    public ResourceLoader()
    {
        int hr = NativeMethods.MrmCreateResourceManager(GetDefaultResourcePath(), out this.resourceManagerHandle);
        Marshal.ThrowExceptionForHR(hr);

        hr = NativeMethods.MrmGetChildResourceMap(this.resourceManagerHandle, IntPtr.Zero, "Resources", out this.resourceMapHandle);
        Marshal.ThrowExceptionForHR(hr);
    }

    public ResourceLoader(string filename)
    {
        int hr = NativeMethods.MrmCreateResourceManager(filename, out this.resourceManagerHandle);
        Marshal.ThrowExceptionForHR(hr);

        hr = NativeMethods.MrmGetChildResourceMap(this.resourceManagerHandle, IntPtr.Zero, "Resources", out this.resourceMapHandle);
        Marshal.ThrowExceptionForHR(hr);
    }

    public ResourceLoader(string filename, string resourceMap)
    {
        int hr = NativeMethods.MrmCreateResourceManager(GetDefaultResourcePath(), out this.resourceManagerHandle);
        Marshal.ThrowExceptionForHR(hr);

        hr = NativeMethods.MrmGetChildResourceMap(this.resourceManagerHandle, IntPtr.Zero, resourceMap, out this.resourceMapHandle);
        Marshal.ThrowExceptionForHR(hr);
    }

    ~ResourceLoader()
    {
        NativeMethods.MrmDestroyResourceManager(this.resourceManagerHandle);
    }

    public static string GetDefaultResourcePath()
    {
        int hr = NativeMethods.MrmGetFilePathFromName("resources.pri", out IntPtr pathPtr);
        Marshal.ThrowExceptionForHR(hr);

        string? path = Marshal.PtrToStringUni(pathPtr);
        NativeMethods.MrmFreeResource(pathPtr);

        if (path == null)
        {
            throw new InvalidOperationException("MrmGetFilePathFromName() returned NULL string");
        }

        return path;
    }

    public string GetString(string resourceId)
    {
        int hr = NativeMethods.MrmLoadStringResource(this.resourceManagerHandle, IntPtr.Zero, this.resourceMapHandle,
            resourceId, out IntPtr valuePtr);
        Marshal.ThrowExceptionForHR(hr);

        string? value = Marshal.PtrToStringUni(valuePtr);
        NativeMethods.MrmFreeResource(valuePtr);

        if (value == null)
        {
            throw new InvalidOperationException("MrmLoadStringResource() returned NULL string");
        }

        return value;
    }

    public string GetStringForUri(Uri uri)
    {
        int hr = NativeMethods.MrmLoadStringFromResourceUri(this.resourceManagerHandle, IntPtr.Zero, this.resourceMapHandle, uri.ToString(), out IntPtr valuePtr);
        Marshal.ThrowExceptionForHR(hr);

        string? value = Marshal.PtrToStringUni(valuePtr);
        NativeMethods.MrmFreeResource(valuePtr);

        if (value == null)
        {
            throw new InvalidOperationException("MrmLoadStringResource() returned NULL string");
        }

        return value;
    }
}
