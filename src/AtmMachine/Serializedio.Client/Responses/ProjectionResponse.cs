using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Serializedio.Client.Responses
{
    public class ProjectionResponse<T>
    {
        public Guid ProjectionId { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public T Data { get; set; }
    }
}
