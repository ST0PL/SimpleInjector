using System.Diagnostics;
using SimpleInjector;

namespace InjectorExample;
class Program
{
    public static void Main(string[] args)
    {
        string processName = "InjectorExample";

        Console.WriteLine($"Waiting for {processName}.exe...");

        while (Process.GetProcessesByName(processName).Length < 1)
            Thread.Sleep(100);

        Console.Clear();
        Console.WriteLine("Injecting...");

        Core.InjectDll(processName, Path.GetFullPath("ExampleCpp.dll"), "Initialize", arg: "Boo!;Hello from C++!");
        Core.InjectDll(processName, Path.GetFullPath("ExampleNativeAOT.dll"), "Initialize", arg: "Boo!;Hello from C#!");

        Console.WriteLine("Success!");
        Console.Write("Press any key for exit...");
        Console.ReadLine();
    }
}