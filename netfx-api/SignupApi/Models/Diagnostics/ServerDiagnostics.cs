using System;
using System.Collections.Generic;

namespace SignupApi.Models
{
    public class ServerDiagnostics
    {
        public string ApplicationName { get; set; }

        public string ApplicationVersionNumber { get; set; }

        public string Status { get; set; }

        public string WorkingDirectory { get; set; }

        public string DnsHostName { get; set; }

        public string MachineName { get; set; }

        public DateTime MachineDate { get; set; }

        public string MachineCulture { get; set; }

        public string MachineTimeZone { get; set; }

        public IEnumerable<string> IpAddressList { get; set; }

        public IEnumerable<DiagnosticCheckCollection> Checks { get; set; }
    }
}