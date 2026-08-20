using System.Runtime.InteropServices;

namespace Microsoft.ApplicationModel.Resources;

internal static class NativeMethods
{
    #region Error Codes

    public static readonly int E_NOT_FOUND = HRESULT_FROM_WIN32(1168);
    public static readonly int E_FILE_NOT_FOUND = HRESULT_FROM_WIN32(2);
    public static readonly int E_PATH_NOT_FOUND = HRESULT_FROM_WIN32(3);
    public static readonly int E_MRM_MAP_NOT_FOUND = HRESULT_FROM_WIN32(15135);
    public static readonly int E_MRM_NAMED_RESOURCE_NOT_FOUND = HRESULT_FROM_WIN32(15127);
    public static readonly int E_MRM_NO_CANDIDATE = HRESULT_FROM_WIN32(15115);
    public static readonly int E_MRM_NO_MATCH_OR_DEFAULT_CANDIDATE = HRESULT_FROM_WIN32(15116);

    public static bool IsResourceNotFound(int hr)
    {
        return hr == E_NOT_FOUND || hr == E_FILE_NOT_FOUND || hr == E_PATH_NOT_FOUND ||
            hr == E_MRM_MAP_NOT_FOUND || hr == E_MRM_NAMED_RESOURCE_NOT_FOUND ||
            hr == E_MRM_NO_CANDIDATE || hr == E_MRM_NO_MATCH_OR_DEFAULT_CANDIDATE;
    }

    private static int HRESULT_FROM_WIN32(int error)
    {
        if (error <= 0)
        {
            return error;
        }
        else
        {
            const int FACILITY_WIN32 = 7;
            return unchecked((int)((error & 0xFFFF) | (FACILITY_WIN32 << 16) | 0x80000000));
        }
    }

    #endregion

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
