using System;
using System.Collections.Generic;
using System.Text;

namespace Serializedio.Client.Types
{
    public class ProjectionDefinition
    {
        public string EventType { get; set; }

        public List<ProjectionDefinitionFunction> Functions { get; set; }
    }
}
