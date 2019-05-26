using System;
using System.Runtime.InteropServices;

namespace RdtscExample
{
    class Program
    {
        const uint PAGE_EXECUTE_READWRITE = 0x40;
        const uint MEM_COMMIT = 0x1000;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress,
            uint dwSize,
            uint flAllocationType,
            uint flProtect);

        static IntPtr Alloc(byte[] asm)
        {
            var ptr = VirtualAlloc(IntPtr.Zero,
                (uint)asm.Length,
                MEM_COMMIT,
                PAGE_EXECUTE_READWRITE);
            Marshal.Copy(asm, 0, ptr, asm.Length);
            return ptr;
        }

        delegate long RdtscDelegate();

        static readonly byte[] rdtscAsm =
        {
            0x0F, 0x31, // RDTSC
            0xC3        // RET
        };

        static void Main()
        {
            var rdtsc = Marshal
                .GetDelegateForFunctionPointer<RdtscDelegate>(Alloc(rdtscAsm));
            Console.WriteLine(rdtsc());
        }
    }
}