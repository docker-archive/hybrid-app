using System.Collections.Generic;

namespace SignupApi.Models
{
    public class DiagnosticCheckResult
    {
        public string CheckName { get; set; }

        public bool Passed { get; set; }
        
        public string Notes { get; set; }
    }
}