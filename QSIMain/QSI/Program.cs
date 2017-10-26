using QuantumRuntime;
using QuantumToolkit.Parser;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace QSI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Disable Close Button
            DisableCloseButton(Console.Title);

            var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var inputFile = Path.Combine(exeDir, @"..\..\QSI_Code\Test.cs");
            var generator = new Generator(File.ReadAllText(inputFile));
            generator.Parse("Test");
            generator.MatRepANDAnalysis(false);
            //Console.WriteLine(generator.OperatorGenerator);

            QAsm.Generate("Test", 0, 1, QuantumMath.PreSKMethod.OrginalQSD, generator.OperatorGenerator.OperatorTree);
            QAsm.WriteQAsmText(true);

            var test = QEnv.CreateQEnv<QSI_Code.Test>();
            test.DisplayRegisterSet = false;



            test.Init();
            test.Run();


            //    test.InitSuperRegister();


            test.InitSuperRegister();
            Console.WriteLine("Count number of ONE is {0}\n", test.r1.Value);
            Console.ReadKey(true);
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        static extern IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
        public static void DisableCloseButton(string consoleName)
        {
            IntPtr windowHandle = FindWindow(null, consoleName);
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            RemoveMenu(closeMenu, 0xF060, 0x0);
        }
    }
}
