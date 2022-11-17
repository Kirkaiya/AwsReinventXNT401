using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Reflection;

namespace BenchmarkWebAPI.Hosting
{
    public class HostInfoService {
        private static readonly Process _process = Process.GetCurrentProcess();
        private static readonly string _hostName = Dns.GetHostName();
        private static bool _isRunningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != null;
        private static IEnumerable<string> _ipAddresses = new List<string>(Dns.GetHostAddresses(_hostName).Select(a => a.ToString()));
        private static readonly string _buildId = ((AssemblyInformationalVersionAttribute)typeof(HostInfoService).Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true)[0]).InformationalVersion;

        public HostInfo GetHostInfo() {
            var hostInfo = new HostInfo {
                BuildId = $"You guys are awesome {_buildId}",
                HostName = _hostName,
                IsRunningInContainer = _isRunningInContainer,
                FrameworkDescription = RuntimeInformation.FrameworkDescription,
                ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString(),
                OSDescription = RuntimeInformation.OSDescription,
                ProcessorCount = Environment.ProcessorCount,
                IPAddresses = _ipAddresses,
                WorkingSet64 = _process.WorkingSet64,
                MaxWorkingSet = (long)_process.MaxWorkingSet
            };

            if (RuntimeInformation.OSDescription.StartsWith("Linux") && Directory.Exists("/sys/fs/cgroup/memory")) {
                hostInfo.CGroupMemoryUsage = System.IO.File.ReadAllLines("/sys/fs/cgroup/memory/memory.usage_in_bytes")[0];
            }

            return hostInfo;
        }
    }
}