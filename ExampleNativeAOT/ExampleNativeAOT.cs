using System.Runtime.InteropServices;

namespace ExampleNativeAOT
{
    public static partial class ExampleNativeAOT
    {
        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
        public static partial int MessageBoxW(nint hWnd, string lpText, string lpCaption, uint uType);

        [UnmanagedCallersOnly(EntryPoint = "Initialize")]
        public static void Initialize(nint args)
        {
            var strings = Marshal.PtrToStringUni(args)!.Split(";");
            MessageBoxW(nint.Zero, strings[1], strings[0], 0x0);
        }
    }
}
