using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace AtmMachine.Infrastructure.Contexts.Options
{
    public class EsConnectionOptions
    {
        public string ConnectionString { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
