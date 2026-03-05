using Domain.BankAccount.Domain.Entities;
using Domain.BankAccount.Domain.Entities.Exceptions;
using Domain.BankAccount.Domain.Enums;
using Domain.BankAccount.Domain.Exceptions;
namespace Domain.BankAccount.Domain.Entities;

public class BankClient
{
    public Guid Id { get;}
    
    public string Name { get;}

    private readonly ICollection<BankAccount> accounts = [];

    public IReadOnlyCollection<BankAccount> Accounts =>
        accounts.Where(a => a.Status == BankAccountStatus.Active).ToList().AsReadOnly(); // только автивные счета

    public BankClient(BankAccount bankAccount)
    {
        accounts.Add(bankAccount);
    }

    public BankClient(string name)
    { 
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        
        Name = name;
    }

    public BankAccount CreateBankAccount()
    { 
        var bankAccount = new BankAccount(this);
        accounts.Add(bankAccount);
        return bankAccount;
    }

    public bool CloseBankAccount(BankAccount account)
    { 
        if(account == null)
            throw new NoValueException(nameof(account));
        if (!accounts.Contains(account))
            throw new InvalidOperationException("This account does not belong to this client");
        if (account.Status == BankAccountStatus.Closed)
            throw new InvalidOperationException("This account is already closed");

        account.CloseAccount();
        return true;
    }

    public bool FrozeBankAccount(BankAccount account)
    { 
        if(account == null)
            throw new ArgumentNullException(nameof(account));
        if (!accounts.Contains(account))
            throw new InvalidOperationException("This account does not belong to this client");
        if (account.Status == BankAccountStatus.Closed)
            throw new InvalidOperationException("This account is closed");
        if(account.Status == BankAccountStatus.Frozen)
            throw new InvalidOperationException("This account is already frozen");
 
        account.FreezeAccount();
        return true;
    }

    public Transaction MakeDeposit(BankAccount account, decimal amount, string note)
    { 
        if(account == null)
            throw new ArgumentNullException(nameof(account));
        if (!accounts.Contains(account))
            throw new InvalidOperationException("This account does not belong to this client");
        return account.MakeDeposit(amount, DateTime.UtcNow, note);
    }

    public Transaction MakeWithdrawal(BankAccount account, decimal amount, string note)
    {
        if(account == null)
            throw new ArgumentNullException(nameof(account));
        if (!accounts.Contains(account))
            throw new InvalidOperationException("This account does not belong to this client");
        return account.Withdrawal(amount, DateTime.UtcNow, note);

    }

    public Transaction MakeTransfer(BankAccount source, BankAccount distination, decimal amount, string note, TransactionStatus status = TransactionStatus.Tranfer)
    {
        if(source == null)
            throw new ArgumentNullException(nameof(source));
        if(distination == null)
            throw new ArgumentNullException(nameof(distination));
        if (!accounts.Contains(source))
            throw new NoSuchAccount(source);
        if (!accounts.Contains(distination))
            throw new NoSuchAccount(distination);
        if (source == distination)
            throw new InvalidOperationException("Source and distination accounts must be different");
        if (source.Status == BankAccountStatus.Closed || distination.Status == BankAccountStatus.Closed)
            throw new InvalidOperationException("Source or distination account is closed");
        if(source.Status == BankAccountStatus.Frozen || distination.Status == BankAccountStatus.Frozen)
            throw new InvalidOperationException("Source or distination account is frozen");


        source.Withdrawal(amount, DateTime.UtcNow, note);
        return distination.MakeDeposit(amount, DateTime.UtcNow, note);

    }

    // написать свойство для просмотра сумму денег по всем своим активным счетам
    public decimal TotalBalance => accounts.Where(a => a.Status == BankAccountStatus.Active).Sum(a => a.Balance);
}
