using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.EntityFrameworkCore.Models
{
    public class NLog
    {
        public string MachineName { get; set; }

        public string SiteName { get; set; }

        public string Logged { get; set; }

        public string Level { get; set; }

        public string UserName { get; set; }

        public string Message { get; set; }

        public string Logger { get; set; }

        public string Properties { get; set; }

        public string Host { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Url { get; set; }

        public string CallSite { get; set; }

        public string Exception { get; set; }
    }
}
