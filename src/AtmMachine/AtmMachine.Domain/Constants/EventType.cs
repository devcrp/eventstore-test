using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.Constants
{
    public static class EventType
    {
        public const string Deposit = nameof(Deposit);
        public const string Withdraw = nameof(Withdraw);
    }
}
