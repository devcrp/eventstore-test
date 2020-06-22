using System;
using System.Collections.Generic;
using System.Text;

namespace Serializedio.Client.Types
{
    public class ProjectionDefinitionFunction
    {
        public string Function { get; set; }
        public string TargetSelector { get; set; }
        public string EventSelector { get; set; }
    }
}
