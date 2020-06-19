using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Shared.Dtos
{
    public class AccountCreationDto
    {
        public decimal InitialDeposit { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
