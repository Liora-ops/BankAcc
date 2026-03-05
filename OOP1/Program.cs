using Domain.BankAccount.Domain.Entities;
namespace OOP1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //BankClient client = new BankClient("asdas");
                //BankAccount account1 = client.CreateBankAccount();
                //BankAccount account2 = client.CreateBankAccount();
                //client.MakeDeposit(account1, 1000, "asdas");
                //Console.WriteLine(client.TotalBalance);

                //client.MakeWithdrawal(account1, 100, "asdas");
                //Console.WriteLine(client.TotalBalance);
                //Console.WriteLine(account1.GetTransactionsHistory());
                //client.FrozeBankAccount(account1);
                //client.MakeTransfer(account1, account2, 900, "asdas");
                //Console.WriteLine(client.TotalBalance);
                BankAccount account1 = new BankAccount();
                BankClient client = new BankClient(account1);
                client.CreateBankAccount();

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.ParamName + " " + ex.ActualValue + " " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
