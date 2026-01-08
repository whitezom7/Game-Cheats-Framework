using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Game_Cheats_Framework
{
    public class Memory : IDisposable
    {
        private IntPtr _handle;
        private Process _process;
        private bool _isAttached = false;





        public Memory(string processName)
        {
            var processes = Process.GetProcessesByName(processName);

            if (processes.Length == 0)
            {
                throw new Exception($"Could not find process: {processName}.");
            }

            _process = processes[0];

            _handle = Natives.OpenProcess(
                   Natives.ProcessorAccessFlags.AllAccess,
                   false,
                   _process.Id
               );

            if (_handle == IntPtr.Zero) 
            {
                throw new Exception("Failed to open Process, Try running as Admin");
            }

            _isAttached = true;

        }

        public IntPtr BaseAddress
        {
            get
            {
                return _process.MainModule!.BaseAddress;
            }
        }

        public T Read<T>(IntPtr address) where T : struct
        {
            int size = Marshal.SizeOf<T>();

            byte[] buffer = new byte[size];

            Natives.ReadProcessMemory(_handle, address, buffer, size, out int bytesRead);

            return MemoryMarshal.Read<T>(buffer);
        }

        public void Write<T>(IntPtr address, T value) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));

            byte[] buffer = new byte[size];

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            Marshal.StructureToPtr(value ,handle.AddrOfPinnedObject(), false);

            handle.Free();

            Natives.WriteProcessMemory(_handle, address, buffer, size, out int bytesWritten);
        }

        public void Dispose()
        {
            if (_handle != IntPtr.Zero)
            {
                Natives.CloseHandle(_handle);

                _handle = IntPtr.Zero;
            }

            GC.SuppressFinalize(this);
        }


    }
}
