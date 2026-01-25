using System.Runtime.InteropServices;

namespace SimpleInjector
{
    public unsafe partial class Native
    {

        [LibraryImport("kernel32.dll", SetLastError = true)]
        public static partial IntPtr OpenProcess(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        public static partial IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, nuint dwSize, uint flAllocationType, uint flProtect);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, nuint nSize, out nuint lpNumberOfBytesWritten);

        [LibraryImport("kernel32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
        public static partial IntPtr GetModuleHandleW(string lpModuleName);

        [LibraryImport("kernel32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf8)]
        public static partial IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        public static partial IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, nuint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        
        [LibraryImport("kernel32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
        public static partial IntPtr LoadLibraryExW(string lpLibFileName, IntPtr hFile, uint dwFlags);
        
        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool FreeLibrary(IntPtr hModule);
        
        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, nuint dwSize, uint dwFreeType);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        public static partial uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        public const uint PROCESS_ALL_ACCESS = 0x001F0FFF;
        public const uint MEM_COMMIT = 0x00001000;
        public const uint MEM_RESERVE = 0x00002000;
        public const uint MEM_RELEASE = 0x00008000;
        public const uint PAGE_READWRITE = 0x0004;
        public const uint DONT_RESOLVE_DLL_REFERENCES = 0x00000001;
        public const uint INFINITE = 0xFFFFFFFF;
    }
}
