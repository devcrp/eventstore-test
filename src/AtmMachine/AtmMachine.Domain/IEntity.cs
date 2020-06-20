using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
