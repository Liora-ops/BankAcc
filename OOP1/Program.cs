using Domain.BankAccount.Domain.Entities;
namespace OOP1
{//asdlja;jdlkasdj;als
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BankClient client = new BankClient("asdas");
                BankAccount account1 = client.CreateBankAccount();
                BankAccount account2 = client.CreateBankAccount();
                client.MakeDeposit(account1, 1000, "asdas");
                Console.WriteLine(client.TotalBalance);

                client.MakeWithdrawal(account1, 100, "asdas");
                Console.WriteLine(client.TotalBalance);
                Console.WriteLine(account1.GetTransactionsHistory());
                client.FrozeBankAccount(account1);
                client.MakeTransfer(account1, account2, 900, "asdas");
                Console.WriteLine(client.TotalBalance);


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
