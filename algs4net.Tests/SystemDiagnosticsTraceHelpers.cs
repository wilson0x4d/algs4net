namespace System.Diagnostics
{
    public static class SystemDiagnosticsTraceHelpers
    {
        public static T Trace<T>(this T obj)
        {
#if DEBUG
            System.Diagnostics.Trace.WriteLine(obj);
#endif
            return obj;
        }
    }
}
