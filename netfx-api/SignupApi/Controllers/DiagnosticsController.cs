using NLog;
using SignupApi.Entities;
using SignupApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignupApi.Controllers
{
    public class DiagnosticsController : ApiController
    {
        private static Logger _Logger = LogManager.GetCurrentClassLogger();

        [ResponseType(typeof(ServerDiagnostics))]
        public IHttpActionResult Get()
        {
            _Logger.Trace("Fetch diagnostics started");

            var diagnostics = new ServerDiagnostics();

            try
            {
                diagnostics.MachineDate = DateTime.Now;
                diagnostics.MachineName = Environment.MachineName;
                diagnostics.MachineCulture = string.Format("{0} - {1}", CultureInfo.CurrentCulture.DisplayName, CultureInfo.CurrentCulture.Name);
                diagnostics.MachineTimeZone = TimeZone.CurrentTimeZone.IsDaylightSavingTime(diagnostics.MachineDate) ? TimeZone.CurrentTimeZone.DaylightName : TimeZone.CurrentTimeZone.StandardName;
                diagnostics.DnsHostName = Dns.GetHostName();
                diagnostics.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var ipHost = Dns.GetHostEntry(diagnostics.DnsHostName);
                var ipList = new List<string>(ipHost.AddressList.Length);
                foreach (var ipAddress in ipHost.AddressList)
                {
                    ipList.Add(ipAddress.ToString());
                }
                diagnostics.IpAddressList = ipList;
                diagnostics.ApplicationName = "Signup API";
                diagnostics.ApplicationVersionNumber = GetType().Assembly.GetName().Version.ToString();

                var dbChecks = new DiagnosticCheckCollection
                {
                    CollectionName = "Data access",
                    Results = GetDbCheckResults()
                };

                diagnostics.Checks = new List<DiagnosticCheckCollection>
                {
                    dbChecks
                };

                SetStatus(diagnostics);
                _Logger.Trace("Fetch diagnostics completed");
            }
            catch (Exception ex)
            {
                diagnostics.Status = string.Format("FAIL - diagnostic check threw error: {0}", ex);
                _Logger.Error(ex, "Fetch diagnostics failed");
            }

            return Ok(diagnostics);
        }

        private IEnumerable<DiagnosticCheckResult> GetDbCheckResults()
        {
            var result = new DiagnosticCheckResult
            {
                CheckName = "Database connection"
            };
            try
            {
                using (var db = new SignUpContext())
                {
                    var dummy = db.Users.FirstOrDefault(x => x.Id == -1);
                    result.Passed = true;
                }
            }
            catch (Exception ex)
            {
                result.Passed = false;
                result.Notes = $"Ex: {ex}";
            }
            return new List<DiagnosticCheckResult> { result };
        }

        protected virtual void SetStatus(ServerDiagnostics diagnostics)
        {
            //overall status:
            var statusBuilder = new StringBuilder();
            foreach (var collection in diagnostics.Checks)
            {
                if (collection.Results.Any(x=>x.Passed == false))
                {
                    statusBuilder.AppendFormat("{0} checks failed; ", collection.CollectionName);
                }
            }
            diagnostics.Status = statusBuilder.ToString();
            if (diagnostics.Status.Length > 0)
            {
                diagnostics.Status = string.Format("RED - {0}", diagnostics.Status);
            }
            else
            {
                diagnostics.Status = "GREEN - service checks all passed";
            }
        }
    }
}
