using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace jsmhToolChest.Libraries
{
    public class NewInjector
    {
        // IMPORTS
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(IntPtr dwDesiredAccess, bool bInheritHandle, uint processId);

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, char[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, ref IntPtr lpThreadId);

        public static void Inject(string path, int ProcessID)
        {
            var processes = Process.GetProcessById(ProcessID);
            var process = processes.Responding;

            IntPtr handle = OpenProcess((IntPtr)2035711, false, (uint)ProcessID);

            IntPtr p1 = VirtualAllocEx(handle, IntPtr.Zero, (uint)(path.Length + 1), 12288U, 64U);
            WriteProcessMemory(handle, p1, path.ToCharArray(), path.Length, out IntPtr p2);
            IntPtr procAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            IntPtr p3 = CreateRemoteThread(handle, IntPtr.Zero, 0U, procAddress, p1, 0U, ref p2);
        }
    }
    internal class DLLInjector
    {
        public static void InjectDLL(string Filename, byte[] DLL, Process TargetProcess)
        {
            try
            {
                System.IO.File.WriteAllBytes(Config.Config_folder + "\\DLLInjector.exe", Resource1.Injector);
                System.IO.File.WriteAllBytes(Config.Config_folder + "\\" + Filename, DLL);
                
            }
            catch (Exception e)
            {
                throw new Exception("文件写出失败，详细信息:" + e.Message);
            }
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = Config.Config_folder + "\\DLLInjector.exe";
                process.StartInfo.Arguments = $"{TargetProcess.Id} {Filename}";
                process.StartInfo.WorkingDirectory = Config.Config_folder;
                process.Start();
            } catch (Exception e)
            {
                throw new Exception("注入器进程创建失败: " + e.Message);
            }

        }
    }
}
