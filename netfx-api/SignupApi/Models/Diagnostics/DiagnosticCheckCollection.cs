using System.Collections.Generic;

namespace SignupApi.Models
{
    public class DiagnosticCheckCollection
    {
        public string CollectionName { get; set; }

        public IEnumerable<DiagnosticCheckResult> Results { get; set; }
    }
}