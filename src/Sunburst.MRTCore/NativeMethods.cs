using System.Runtime.InteropServices;

namespace Microsoft.ApplicationModel.Resources;

internal static class NativeMethods
{
    public struct MrmResourceData
    {
        public uint size;
        public IntPtr data;
    }

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmCreateResourceManager(string fileName, out IntPtr handle);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true)]
    public static extern void MrmDestroyResourceManager(IntPtr handle);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true)]
    public static extern int MrmCreateResourceContext(IntPtr managerHandle, out IntPtr contextHandle);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true)]
    public static extern void MrmFreeQualifierNamesOrValues(uint size,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] names);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true)]
    public static extern int MrmGetAllQualifierNames(IntPtr contextHandle, uint size,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] ref string[] names);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmGetQualifier(IntPtr contextHandle, string qualifierName, out string value);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmSetQualifier(IntPtr contextHandle, string qualifierName, string value);

    [DllImport("MMR.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true)]
    public static extern void MrmDestroyResourceContext(IntPtr contextHandle);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmGetChildResourceMap(IntPtr managerHandle, [Optional] IntPtr mapHandle,
        string mapName, out IntPtr childMapHandle);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmGetResourceCount(IntPtr managerHandle, [Optional] IntPtr mapHandle, out uint count);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmLoadStringResource(
        IntPtr managerHandle, [Optional] IntPtr contextHandle, [Optional] IntPtr mapHandle,
        string resourceId, out string value);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmLoadStringFromResourceUri(IntPtr managerHandle, [Optional] IntPtr contextHandle,
        [Optional] IntPtr mapHandle, string uri, out string value);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmLoadEmbeddedResource(IntPtr managerHandle, [Optional] IntPtr contextHandle,
        [Optional] IntPtr mapHandle, string resourceId, out MrmResourceData data);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmLoadEmbeddedResourceFromResourceUri(IntPtr managerHandle,
        [Optional] IntPtr contextHandle, [Optional] IntPtr mapHandle, string uri, out MrmResourceData data);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmLoadStringOrEmbeddedResource(IntPtr managerHandle, [Optional] IntPtr contextHandle,
        [Optional] IntPtr mapHandle, string resourceId, out ResourceCandidateKind kind,
        [Optional] out string stringValue, out MrmResourceData dataValue);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmLoadStringOrEmbeddedResourceWithQualifierValues(IntPtr managerHandle, [Optional] IntPtr contextHandle,
        [Optional] IntPtr mapHandle, string resourceId, out ResourceCandidateKind kind, [Optional] out string stringValue,
        out MrmResourceData data, out uint qualifierCount,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 7)] out string[] qualifierNames,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 7)] out string[] qualifierValues);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmLoadStringOrEmbeddedResourceFromResourceUri(IntPtr managerHandle,
        [Optional] IntPtr contexthandle, [Optional] IntPtr mapHandle, string resourceUri, out ResourceCandidateKind kind,
        [Optional] out string resourceString, out MrmResourceData data);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmLoadStringOrEmbeddedResourceByIndex(IntPtr managerHandle, [Optional] IntPtr contextHandle,
        [Optional] IntPtr mapHandle, uint index, out ResourceCandidateKind kind, [Optional] out string stringValue,
        out MrmResourceData data);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmLoadStringOrEmbeddedResourceByIndexWithQualifierValues(IntPtr managerHandle,
        [Optional] IntPtr contextHandle, [Optional] IntPtr mapHandle, uint index, out ResourceCandidateKind kind,
        [Optional] out string stringValue, out MrmResourceData data, out uint qualifierCount,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 7)] out string[] qualifierNames,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 7)] out string[] qualifierValues);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true)]
    public static extern IntPtr MrmAllocateBuffer(uint size);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true)]
    public static extern void MrmFreeResource(IntPtr buffer);

    [DllImport("MRM.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, PreserveSig = true)]
    public static extern int MrmGetFilePathFromName(string filename, out string path);
}
