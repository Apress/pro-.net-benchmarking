using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using RuntimeEnvironment = Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment;

namespace GetUnixVersion
{
    class Program
    {
        [DllImport("libc", SetLastError = true)]
        private static extern int uname(IntPtr buf);

        private static string GetSysnameFromUname()
        {
            var buf = IntPtr.Zero;
            try
            {
                buf = Marshal.AllocHGlobal(8192);
                // This is a hacktastic way of getting sysname from uname ()
                int rc = uname(buf);
                if (rc != 0)
                {
                    throw new Exception("uname from libc returned " + rc);
                }

                string os = Marshal.PtrToStringAnsi(buf);
                return os;
            }
            finally
            {
                if (buf != IntPtr.Zero)
                    Marshal.FreeHGlobal(buf);
            }
        }

        private static void Run(string command, string args)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = command,
                WorkingDirectory = "",
                Arguments = args,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            using (var process = new Process {StartInfo = processStartInfo})
            {
                try
                {
                    process.Start();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }

                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                
                Console.WriteLine($"$ {command} {args}");
                Console.WriteLine(output);
            }
        }

        static void Main()
        {
            Console.WriteLine("Environment.OSVersion                      = " + Environment.OSVersion);
            Console.WriteLine("Sysname                                    = " + GetSysnameFromUname());
            Console.WriteLine("RuntimeEnvironment.OperatingSystem         = " + RuntimeEnvironment.OperatingSystem);
            Console.WriteLine("RuntimeEnvironment.OperatingSystemPlatform = " + RuntimeEnvironment.OperatingSystemPlatform);
            Console.WriteLine("RuntimeEnvironment.OperatingSystemVersion  = " + RuntimeEnvironment.OperatingSystemVersion);
            Console.WriteLine("RuntimeInformation.OSDescription           = " + RuntimeInformation.OSDescription);



            if (GetSysnameFromUname() == "Linux")
            {
                Run("lsb_release", "-a");
            }
            
            if (GetSysnameFromUname() == "Darwin")
            {
                Run("sw_vers", "-productVersion");
                Run("system_profiler", "SPSoftwareDataType");
            }
        }
    }
}