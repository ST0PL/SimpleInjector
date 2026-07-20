using System.Diagnostics;
using System.Text;

namespace SimpleInjector
{
    public static class Core
    {
        public static nint WriteString(nint processHandle, string str)
        {
            if (string.IsNullOrEmpty(str))
                return IntPtr.Zero;

            byte[] utf16 = Encoding.Unicode.GetBytes(str+'\0');
            nint strAddr = Native.VirtualAllocEx(processHandle, 0, (nuint)utf16.Length, Native.MEM_RESERVE | Native.MEM_COMMIT, Native.PAGE_READWRITE);
            Native.WriteProcessMemory(processHandle, strAddr, utf16, (nuint)utf16.Length, out _);
            return strAddr;
        }

        public static nint GetModuleBase(int processId, string moduleName)
        {
            var modules = Process.GetProcessById(processId).Modules;

            foreach (ProcessModule module in modules)
            {
                if (string.Equals(module.ModuleName, moduleName))
                    return module.BaseAddress;
            }
            return 0;
        }

        public static nint GetInitProcOffset(string dllPath, string entryPoint)
        {
            var libHandle = Native.LoadLibraryExW(dllPath, IntPtr.Zero, Native.DONT_RESOLVE_DLL_REFERENCES);
            var initAddress = Native.GetProcAddress(libHandle, entryPoint);
            Native.FreeLibrary(libHandle);
            return initAddress - libHandle;
        }

        public static void Execute(nint processHandle, nint moduleBase, nint initProcOffset, string arg)
            => Native.CreateRemoteThread(processHandle, IntPtr.Zero, 0, moduleBase + initProcOffset, WriteString(processHandle, arg), 0, IntPtr.Zero);

        public static (nint ProcessHandle, nint ModuleBase) LoadDll(string processName, string dllPath)
        {
            int processId = Process.GetProcessesByName(processName).FirstOrDefault()?.Id ?? throw new InvalidOperationException($"Process with name \"{processName}\" not found.");
            nint processHandle = Native.OpenProcess(Native.PROCESS_ALL_ACCESS, false, processId);

            nint pathAddr = WriteString(processHandle, dllPath);
            nint loadLibraryProcAddr = Native.GetProcAddress(Native.GetModuleHandleW("kernel32.dll"), "LoadLibraryW");
            nint threadHandle = Native.CreateRemoteThread(processHandle, IntPtr.Zero, 0, loadLibraryProcAddr, pathAddr, 0, IntPtr.Zero);

            Native.WaitForSingleObject(threadHandle, Native.INFINITE);
            Native.VirtualFreeEx(processHandle, pathAddr, 0, Native.MEM_RELEASE);

            return (processHandle, GetModuleBase(processId, Path.GetFileName(dllPath)));
        }

        public static void InjectDll(string processName, string dllPath, string? entryPoint, string arg)
        {
            (nint processHandle, nint moduleBase) = LoadDll(processName, dllPath);

            if (moduleBase == IntPtr.Zero)
                throw new NullReferenceException("Module base was not found.");

            if(!string.IsNullOrWhiteSpace(entryPoint))
                Execute(processHandle, moduleBase, GetInitProcOffset(dllPath, entryPoint), arg);
        }
    }
}
