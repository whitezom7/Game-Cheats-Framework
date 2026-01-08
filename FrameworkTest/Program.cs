using System;
using Game_Cheats_Framework;

namespace FrameworkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var mem = new Memory("CoDWaW");

            IntPtr gameBase = mem.BaseAddress;

            IntPtr fovAddress = IntPtr.Add(gameBase, 0x1DC4F98);

            float fov = mem.Read<float>(fovAddress);
            Console.WriteLine(fov);
        }

    }
}
