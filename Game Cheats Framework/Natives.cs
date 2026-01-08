using System;
using System.Runtime.InteropServices;

namespace Game_Cheats_Framework
{
    internal class Natives
    {
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(
            ProcessorAccessFlags dwDesiredAccess,
            bool bInheritHandle,
            int dwProcessId
            );


        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out int lpNumberOfBytesRead
            );

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            int dwSize,
            out int lpNumberOfBytesRead
            );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [Flags]
        public enum ProcessorAccessFlags : int
        {
            VMRead = 0X0010, // Read Memory
            VMWrite = 0x0020, // Write Memory
            VMOperation = 0x0008, // Required to write memory
            AllAccess = 0x1F0FFF
        }
    }
}
