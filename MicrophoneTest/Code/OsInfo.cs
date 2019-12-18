using System.Linq;
namespace MicrophoneTest.Code
{


    public class MemoryMetrics
    {
        public ulong TotalPhys;
        public ulong AvailPhys;
        public ulong TotalPageFile;
        public ulong AvailPageFile;
        public ulong TotalVirtual;
        public ulong AvailVirtual;
        public ulong AvailExtendedVirtual;


        public double Total;
        public uint Used;
        public double Free;


        public HealthStatus_t HealthStatus
        {
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.healthcheckapplicationbuilderextensions.usehealthchecks?view=aspnetcore-3.1
            // https://gunnarpeipman.com/aspnet-core-memory-health-check/

            get
            {
                if (this.Used > 89)
                {
                    return HealthStatus_t.Unhealthy;
                }

                if (this.Used > 79)
                {
                    return HealthStatus_t.Degraded;
                }

                return HealthStatus_t.Healthy;
            }
        }

    }


    public enum HealthStatus_t
    {
        Healthy,
        Degraded,
        Unhealthy
    }


    public class OsInfo
    {

        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "GetSystemDefaultLangID")]
        public static extern int GetSystemDefaultLangID();

        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "GetSystemDefaultLCID")]
        static extern uint GetSystemDefaultLCID();

        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "GetUserDefaultLCID")]
        private static extern int GetUserDefaultLCID();

        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "GetOEMCP")]
        public static extern int GetOEMCP();


        public static System.Globalization.CultureInfo CurrentCultureInRegionalSettings()
        {
            // System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            // System.Globalization.CultureInfo.CurrentCulture.TextInfo.


            return new System.Globalization.CultureInfo(GetUserDefaultLCID());
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        internal struct MEMORYSTATUSEX
        {
            // The size of the structure, in bytes. 
            // You must set this member before calling
            internal uint dwLength;

            // https://docs.microsoft.com/en-us/windows/win32/api/sysinfoapi/ns-sysinfoapi-memorystatusex
            // A number between 0 and 100 that specifies the approximate percentage of physical memory that is in use 
            // (0 indicates no memory use and 100 indicates full memory use).
            internal uint dwMemoryLoad;

            internal ulong ullTotalPhys;
            internal ulong ullAvailPhys;
            internal ulong ullTotalPageFile;
            internal ulong ullAvailPageFile;
            internal ulong ullTotalVirtual;
            internal ulong ullAvailVirtual;
            internal ulong ullAvailExtendedVirtual;
        }
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        [System.Runtime.InteropServices.DllImport("Kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        internal static extern bool GlobalMemoryStatusEx([System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out] MEMORYSTATUSEX lpBuffer);


        // https://stackoverflow.com/questions/1553336/how-can-i-get-the-total-physical-memory-in-c
        private MemoryMetrics GetWindowsMetrics()
        {
            MEMORYSTATUSEX statEX = new MEMORYSTATUSEX();
            statEX.dwLength = (uint) System.Runtime.InteropServices.Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            GlobalMemoryStatusEx(statEX);
            
            MemoryMetrics mm = new MemoryMetrics();

            mm.Used = statEX.dwMemoryLoad;
            mm.TotalPhys = statEX.ullTotalPhys;
            mm.AvailPhys = statEX.ullAvailPhys;
            mm.TotalPageFile = statEX.ullTotalPageFile;
            mm.AvailPageFile = statEX.ullAvailPageFile;
            mm.TotalVirtual = statEX.ullTotalVirtual;
            mm.AvailVirtual = statEX.ullAvailVirtual;
            mm.AvailExtendedVirtual = statEX.ullAvailExtendedVirtual;
            
            return mm;
        }


        private MemoryMetrics GetUnixMetrics()
        {
            MemoryMetrics mm = new MemoryMetrics();

            // At this point, we already checked if /proc/meminfo exists 
            // https://github.com/ststeiger/ReadMemInfo/blob/master/ReadMemInfo/Proc.cs

            return mm;
        }


        public MemoryMetrics GetMetrics()
        {
            MemoryMetrics metrics = null;

            // X-Box supports GlobalMemoryStatusEx 
            // https://github.com/microsoft/Xbox-ATG-Samples/blob/master/UWPSamples/System/SystemInfoUWP/SystemInfo.cpp
            if (System.Environment.OSVersion.Platform != System.PlatformID.Unix)
            {
                metrics = GetWindowsMetrics();
            }
            else if (System.IO.File.Exists("/proc/meminfo"))
            {
                metrics = GetUnixMetrics();
            }
            else
            {
                throw new System.NotSupportedException("GetMetrics not supported for current operating system.");
            }

            return metrics;
        }


        public enum OSType_t
        {
            Windows,
            Linux,
            OSX,
            Unix,
            XBox,
            UNKNOWN
        }


        public static OSType_t OsType
        {
            get
            {
                if (System.Environment.OSVersion.Platform == System.PlatformID.Xbox)
                    return OSType_t.XBox;

                if (System.Environment.OSVersion.Platform != System.PlatformID.Unix)
                    return OSType_t.Windows;

                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
                    return OSType_t.Linux;

                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
                    return OSType_t.OSX;

                if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                    return OSType_t.Unix;

                return OSType_t.UNKNOWN;
            }

        }



        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        private const int PROCESSOR_ARCHITECTURE_AMD64 = 9;
        private const int PROCESSOR_ARCHITECTURE_IA64 = 6;
        private const int PROCESSOR_ARCHITECTURE_INTEL = 0;

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct SYSTEM_INFO
        {
            public short wProcessorArchitecture;
            public short wReserved;
            public int dwPageSize;
            public System.IntPtr lpMinimumApplicationAddress;
            public System.IntPtr lpMaximumApplicationAddress;
            public System.IntPtr dwActiveProcessorMask;
            public int dwNumberOfProcessors;
            public int dwProcessorType;
            public int dwAllocationGranularity;
            public short wProcessorLevel;
            public short wProcessorRevision;
        }

        public static System.Reflection.ProcessorArchitecture GetProcessorArchitecture()
        {
            SYSTEM_INFO si = new SYSTEM_INFO();
            GetNativeSystemInfo(ref si);
            switch (si.wProcessorArchitecture)
            {
                case PROCESSOR_ARCHITECTURE_AMD64:
                    return System.Reflection.ProcessorArchitecture.Amd64;

                case PROCESSOR_ARCHITECTURE_IA64:
                    return System.Reflection.ProcessorArchitecture.IA64;

                case PROCESSOR_ARCHITECTURE_INTEL:
                    return System.Reflection.ProcessorArchitecture.X86;

                default:
                    return System.Reflection.ProcessorArchitecture.None; // that's weird :-)
            }
        }

        public static void GetProcessorArchitectureSimple()
        {
            switch (typeof(string).Assembly.GetName().ProcessorArchitecture)
            {
                case System.Reflection.ProcessorArchitecture.X86:
                    break;
                case System.Reflection.ProcessorArchitecture.Amd64:
                    break;
                case System.Reflection.ProcessorArchitecture.IA64:
                    break;
                case System.Reflection.ProcessorArchitecture.Arm:
                    break;
            }
        }

        public static void ListInfos()
        {
            //var pc = new System.Diagnostics.PerformanceCounter("Mono Memory", "Available Physical Memory");
            //long availableMemory = pc.RawValue;
            //var pc2 = new System.Diagnostics.PerformanceCounter("Mono Memory", "Total Physical Memory");
            //long physicalMemory = pc2.RawValue;

            // Microsoft.VisualBasic.Devices.ComputerInfo ci = new Microsoft.VisualBasic.Devices.ComputerInfo();

            // typeof(Microsoft.AspNetCore.Mvc.Controller).Assembly.GetName().Version
            // new System.Reflection.AssemblyName(typeof(Microsoft.AspNetCore.Mvc.MvcOptions).Assembly.FullName).Version.ToString();


            // Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion

            // https://github.com/dotnet-architecture/HealthChecks/blob/dev/src/Microsoft.Extensions.HealthChecks/Checks/SystemChecks.cs

            // System.Diagnostics.Process.GetCurrentProcess().WorkingSet64;
            // System.Diagnostics.Process.GetCurrentProcess().MaxWorkingSet;
            // System.Diagnostics.Process.GetCurrentProcess().PeakWorkingSet;
            // System.Diagnostics.Process.GetCurrentProcess().PeakWorkingSet64;

            System.Console.WriteLine(System.Globalization.CultureInfo.InstalledUICulture.EnglishName);

            System.Collections.Generic.List<MemoryMetrics> ls = new System.Collections.Generic.List<MemoryMetrics>();

            ulong[] a = ls.Select(x => x.AvailExtendedVirtual).ToArray();


            System.Console.WriteLine(System.Runtime.InteropServices.RuntimeInformation.OSDescription);
            System.Console.WriteLine(System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture);
            System.Console.WriteLine(System.Runtime.InteropServices.RuntimeInformation.OSArchitecture);
            System.Console.WriteLine(System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
            
            System.Console.WriteLine(System.Environment.Is64BitOperatingSystem);
            System.Console.WriteLine(System.Environment.WorkingSet);
            System.Console.WriteLine(System.Environment.SystemPageSize);
            System.Console.WriteLine(System.Environment.ProcessorCount);
            System.Console.WriteLine(System.Environment.MachineName);
            System.Console.WriteLine(System.Environment.OSVersion.Platform);
            System.Console.WriteLine(System.Environment.OSVersion.VersionString);
            // System.Console.WriteLine(System.Environment.OSVersion.Version.*);
            System.Console.WriteLine(System.Environment.OSVersion.ServicePack);

            System.Console.WriteLine(System.Environment.Is64BitProcess);
            System.Console.WriteLine(System.Environment.TickCount); // uptime
        }


        // https://stackoverflow.com/questions/767613/identifying-the-cpu-architecture-type-using-c-sharp
        public static void GetProcessArchitecture()
        {
            System.Reflection.PortableExecutableKinds peKind;
            System.Reflection.ImageFileMachine machine;
            typeof(object).Module.GetPEKind(out peKind, out machine);

            System.Console.WriteLine(peKind);
            System.Console.WriteLine(machine);
        }


    }
}
