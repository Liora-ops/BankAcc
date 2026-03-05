using Domain.BankAccount.Domain.Entities;
using Domain.BankAccount.Domain.Enums;
namespace Domain.BankAccount.Domain.Entities
{
    public class BankAccount
    {
        private static int countAccount = 0;

        private ICollection<Transaction> transactions = [];

        public IReadOnlyCollection<Transaction> Transactions
            => transactions.ToList();

        public decimal Balance { get; private set; } = 0;
        public BankAccountStatus Status { get; private set; } = BankAccountStatus.Active;

        public BankClient Owner { get; private set; }

        private int _idAccount;

        public BankAccount(BankClient owner)
        {
            Owner = owner;
            _idAccount = countAccount;
            countAccount++;
        }

        public override string ToString()
        {
            return _idAccount + " " + Owner + " " + Balance;
        }

        public BankAccount();

        public Transaction MakeDeposit(decimal amount, DateTime date, string note, TransactionStatus status = TransactionStatus.Deposit)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, $"{_idAccount} amount must be positive");
            if (date > DateTime.UtcNow)
                throw new ArgumentOutOfRangeException(nameof(date), date, $"{_idAccount} date>now");
            Transaction deposit = new Transaction(amount, date, note, status, this);
            Balance += amount;
            transactions.Add(deposit);
            return deposit;
        }
        public bool CloseAccount()
        {
            if (Balance != 0)
                throw new InvalidOperationException($"{_idAccount} Balance must be 0");
            Status = BankAccountStatus.Closed;
            return true;
        }
        public bool FreezeAccount()
        {
            if (Status == BankAccountStatus.Closed)
                throw new InvalidOperationException($"{_idAccount} account is closed");
            Status = BankAccountStatus.Frozen;
            return true;
        }
        public Transaction Withdrawal(decimal amount, DateTime date, string note, TransactionStatus status = TransactionStatus.Withdrawal)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, $"{_idAccount} amount must be positive");
            if (Balance < amount)
                throw new InvalidOperationException($"{_idAccount} Balance < amount");
            if (date > DateTime.UtcNow)
                throw new ArgumentOutOfRangeException(nameof(date), date, $"{_idAccount} date>now");
            Transaction withdrawal = new Transaction(-amount, date, note, status, this);
            Balance -= amount;
            transactions.Add(withdrawal);
            return withdrawal;
        }

        public string GetTransactionsHistory()
            => transactions.Aggregate("", (cur, next) => cur + "\n" + next);
        
    }
}
