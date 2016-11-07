using System;
using System.Diagnostics;

namespace ExplorerReboot
{
    class Program
    {
        static void Main(string[] args)
        {

           // Process.Start("gacutil.exe", " /i Descriptor.Core.dll");
            var process = Process.GetProcessesByName("explorer");
            if (process.Length == 0)
            {
                Console.WriteLine("Process: explorer not found");
                Console.ReadKey(true);
                return;
            }
            if (process.Length > 1)
            {
                Console.WriteLine("To much explorers.");
                Console.ReadKey(true);
                return;
            }
            if (process.Length == 1)
            {
                process[0].Kill();

                Process.Start("explorer.exe");

            }

        }
    }
}
