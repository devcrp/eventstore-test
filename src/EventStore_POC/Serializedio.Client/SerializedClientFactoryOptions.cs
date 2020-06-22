using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Serializedio.Client
{
    public class SerializedClientFactoryOptions
    {
        public Uri BaseUri { get; set; }

        public string AccessKey { get; set; }

        public string SecretAccessKey { get; set; }
    }
}
