namespace Microsoft.ApplicationModel.Resources
{
    public static class KnownResourceQualifierNames
    {
        // NOTE: These must be implemented as properties because, if they're not, it's a
        // binary-incompatible change to the previous (CsWinRT-based) version of this library.

        public static string Contrast { get; } = "Contrast";
        public static string Custom { get; } = "Custom";
        public static string DeviceFamily { get; } = "DeviceFamily";
        public static string HomeRegion { get; } = "HomeRegion";
        public static string Language { get; } = "Language";
        public static string LayoutDirection { get; } = "LayoutDirection";
        public static string Scale { get; } = "Scale";
        public static string TargetSize { get; } = "TargetSize";
        public static string Theme { get; } = "Theme";
    }
}
