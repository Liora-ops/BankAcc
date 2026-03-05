using Domain.BankAccount.Domain.Enums;

namespace Domain.BankAccount.Domain.Entities;

public record Transaction
{
    public decimal Amount { get; }

    public DateTime Date { get; }

    public string Note { get; }

    public TransactionStatus Status { get; }

    public BankAccount? Source { get; } = null;

    public BankAccount? Destination { get; } = null;

    public Guid Id { get; } = Guid.NewGuid();

    private Transaction(decimal amount, DateTime date, string note, TransactionStatus status)
    {
        Amount = amount;
        Date = date;
        Note = note;
        Status = status;
    }

    public Transaction(
        decimal amount,
        DateTime date,
        string note,
        TransactionStatus status,
        BankAccount account)
        :this(amount, date, note, status)
    {
        switch (status)
        {
            case TransactionStatus.Deposit:
                Destination = account ?? throw new NullReferenceException("asd"); 
                break;
            case TransactionStatus.Withdrawal:
                Source = account ?? throw new NullReferenceException("asd");
                break;
            default:
                throw new InvalidOperationException("adsasd");
        }
    }

    public Transaction(
        decimal amount,
        DateTime date,
        string note,
        TransactionStatus status,
        BankAccount source,
        BankAccount destination)
        : this(amount, date, note, status)
    {
        if (status != TransactionStatus.Tranfer)
            throw new InvalidOperationException("sdf");
        
        Source = source ?? throw new NullReferenceException("asd");
        Destination = destination ?? throw new NullReferenceException("asd");
    }

    public override string ToString()
        => $"{Id} {Amount} {Date} {Note} ";
}

