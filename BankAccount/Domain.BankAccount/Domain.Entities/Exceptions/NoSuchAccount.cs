using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.BankAccount.Domain.Entities.Exceptions
{
    public class NoSuchAccount(BankAccount bankAccount) :InvalidOperationException($"The account number {bankAccount.ToString} does not exist in the bank")
    {
        public BankAccount BankAccount => bankAccount;
    }
}
