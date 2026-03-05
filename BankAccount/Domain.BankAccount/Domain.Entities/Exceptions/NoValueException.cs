using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BankAccount.Domain.Entities.Exceptions
{
    public class NoValueException(string IsValue) :InvalidOperationException($"There is no value {IsValue}");
}
