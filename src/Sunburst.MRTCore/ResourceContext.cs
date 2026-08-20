using System.Runtime.InteropServices;

namespace Microsoft.ApplicationModel.Resources;

public sealed class ResourceContext
{
    private readonly IntPtr resourceContextHandle;
    private List<string>? qualifierNames = null;
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
            this.InitializeQualifierValueMap();
            if (this.qualifierValueMap == null)
            {
                throw new InvalidOperationException("Qualifier value map not initialized");
            }

            return this.qualifierValueMap;
        }
    }

    internal void Apply()
    {
        if (this.resourceContextHandle == IntPtr.Zero)
        {
            // Resource not handled by MRT. Nothing to apply.
            return;
        }

        this.InitializeQualifierValueMap();
        if (this.qualifierValueMap == null)
        {
            throw new InvalidOperationException("Qualifier value map not initialized");
        }

        foreach (KeyValuePair<string, string> pair in this.qualifierValueMap)
        {
            if (!string.IsNullOrEmpty(pair.Value))
            {
                int hr = NativeMethods.MrmSetQualifier(this.resourceContextHandle, pair.Key, pair.Value);
                Marshal.ThrowExceptionForHR(hr);
            }
        }
    }

    private unsafe void InitializeQualifierNames()
    {
        if (this.resourceContextHandle != IntPtr.Zero)
        {
            IntPtr* names;
            int hr = NativeMethods.MrmGetAllQualifierNames(this.resourceContextHandle, out uint size, &names);
            Marshal.ThrowExceptionForHR(hr);

            this.qualifierNames = new List<string>();
            for (uint i = 0; i < size; i++)
            {
                IntPtr namePtr = names[i];
                string? name = Marshal.PtrToStringUni(namePtr);
                if (name != null)
                {
                    this.qualifierNames.Add(name);
                }

                NativeMethods.MrmFreeResource(namePtr);
            }

            NativeMethods.MrmFreeResource((IntPtr)names);
        }
        else
        {
            this.qualifierNames = [KnownResourceQualifierNames.Language];
        }
    }

    private void InitializeQualifierValueMap()
    {
        if (this.qualifierNames == null)
        {
            this.InitializeQualifierNames();
        }

        if (this.qualifierNames == null)
        {
            throw new InvalidOperationException("Qualifier names not loaded");
        }

        if (this.qualifierValueMap == null)
        {
            this.qualifierValueMap = new Dictionary<string, string>();

            foreach (string name in this.qualifierNames)
            {
                if (name == KnownResourceQualifierNames.Language)
                {
                    // Override the default behavior.
                    string languages = string.Join(";", Windows.Globalization.ApplicationLanguages.Languages);
                    if (!string.IsNullOrEmpty(languages))
                    {
                        this.qualifierValueMap[name] = languages;
                    }
                }
                else
                {
                    int hr = NativeMethods.MrmGetQualifier(this.resourceContextHandle, name, out IntPtr valuePtr);
                    Marshal.ThrowExceptionForHR(hr);

                    string? value = Marshal.PtrToStringUni(valuePtr);
                    if (value != null)
                    {
                        this.qualifierValueMap[name] = value;
                    }

                    NativeMethods.MrmFreeResource(valuePtr);
                }
            }
        }
        else
        {
            this.qualifierValueMap[KnownResourceQualifierNames.Language] = string.Join(";", Windows.Globalization.ApplicationLanguages.Languages);
        }
    }
}
