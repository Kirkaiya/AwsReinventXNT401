using System.Collections.Generic;

namespace BenchmarkWebAPI.Hosting
{
    public class HostInfo {
        public string BuildId { get; set; }
        public string HostName { get; set; }
        public bool IsRunningInContainer { get; set; }
        public string FrameworkDescription { get; set; }
        public string ProcessArchitecture { get; set; }
        public string OSDescription { get; set; }
        public int ProcessorCount { get; set; }
        public string CGroupMemoryUsage { get; set; }
        public IEnumerable<string> IPAddresses { get; set; }
        public long WorkingSet64 { get; set; }
        public long MaxWorkingSet { get; set; }
    }
}