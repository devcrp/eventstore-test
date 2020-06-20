using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.ValueObjects
{
    public class Event<T>
    {
        public T Data { get; set; }
    }
}
