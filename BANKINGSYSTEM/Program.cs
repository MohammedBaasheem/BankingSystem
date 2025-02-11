using System.Collections;

namespace BANKINGSYSTEM
{
    internal class Program
    {
        class Account
        {
            public long Account_Number { get; set; }
            public decimal Account_Balance { get; set; }
            public string Open_Date { get; set; }
            public Account(long Account_Number, decimal Account_Balance)
            {
                Open_Date = DateTime.Now.ToShortDateString();
                this.Account_Number = Account_Number;
                this.Account_Balance = Account_Balance;
            }
            public virtual void WIthDraw(decimal Amount)
            {
                if (Account_Balance >= Amount)
                {
                    Console.WriteLine("your Balance before Wthdraw : " + Account_Balance);
                    Account_Balance -= Amount;
                    Console.WriteLine("your Balance after Wthdraw : " + Account_Balance);
                }
            }
            public void Deposit(decimal Amount)
            {
                Console.WriteLine("your Balance before Deposit : " + Account_Balance);
                Account_Balance += Amount;
                Console.WriteLine("your Balance after Deposit : " + Account_Balance);
            }
            public virtual void Transfer(ArrayList data_Clients, int index_sender_Client, int index_Receiver_Client, int index_account, decimal Amount)
            {
                ((Clients)data_Clients[index_sender_Client]).MyAccounts[index_account].WIthDraw(Amount);
                ((Clients)data_Clients[index_Receiver_Client]).MyAccounts[index_account].Deposit(Amount);
            }
            public virtual void PrintAccountinfo()
            {
                Console.WriteLine("Account Number : " + Account_Number);
                Console.WriteLine("Account Balance: " + Account_Balance);
                Console.WriteLine("OpenDate : " + Open_Date);
            }
        }

        class CurrentAccount : Account
        {
            public CurrentAccount(long Account_Number, decimal Account_Balance) : base(Account_Number, Account_Balance)
            {
            }
            public override void WIthDraw(decimal Amount)
            {
                if (Account_Balance == 0 || Account_Balance < Amount)
                {
                    Console.WriteLine("sorry your Balance can not WithDraw!!");
                }
                else
                {
                    base.WIthDraw(Amount);
                }
            }
            public override void Transfer(ArrayList data_Clients, int index_sender_Client, int index_Receiver_Client, int index_account, decimal Amount)
            {
                ((Clients)data_Clients[index_sender_Client]).MyAccounts[index_account].WIthDraw(Amount);
                ((Clients)data_Clients[index_Receiver_Client]).MyAccounts[index_account].Deposit(Amount);
            }
            public override void PrintAccountinfo()
            {
                Console.WriteLine("Type of Account : Current Account");
                base.PrintAccountinfo();
            }
        }

        class BusinessAccount : Account
        {
            protected decimal Business;
            public BusinessAccount(long Business, long Account_Number, long Account_Balance) : base(Account_Number, Account_Balance)
            {
                this.Business = Business;
            }
            public override void WIthDraw(decimal Amount)
            {
                if (Account_Balance == 0 && Business != 0 && Amount <= Business)
                {
                    Console.WriteLine("your Balance is: " + 0 + "\nyou can WithDraw from Additional balance ( " + Business + " )");
                    Business -= Amount;
                    Console.WriteLine("Additional balance: " + Business);
                }
                else if ((Amount > Account_Balance && Account_Balance != 0) && Amount <= Business)
                {
                    Console.WriteLine("your before Balance: " + Account_Balance + " is less than " + Amount);
                    Console.WriteLine("you can WithDraw from Additional balance ( " + Business + " ) and your Balance ");
                    Amount -= Account_Balance;
                    Business -= Amount;
                    Account_Balance = 0;
                    Console.WriteLine("your after Balance: " + Account_Balance);
                    Console.WriteLine("your after Additional balance ( " + Business + " )");
                }
                else if (Account_Balance == 0 && Business == 0)
                {
                    Console.WriteLine("sorry your Balance and your Additional balance are : 0 .\nyou can not WithDraw!!");
                }
                else if (Business == 0 && Account_Balance >= Amount)
                {
                    base.WIthDraw(Amount);
                }
                else if (Account_Balance == 0 && Amount > Business)
                {
                    Console.WriteLine("sorry you can not WithDraw your AccountBalance = 0  and " + Amount + " it is larger than the allowable limit.\nthe allowable limit is: " + Business);
                }
            }
            public override void Transfer(ArrayList data_Clients, int index_sender_Client, int index_Receiver_Client, int index_account, decimal Amount)
            {
                ((Clients)data_Clients[index_sender_Client]).MyAccounts[index_account].WIthDraw(Amount);
                ((Clients)data_Clients[index_Receiver_Client]).MyAccounts[index_account].Deposit(Amount);
            }
            public override void PrintAccountinfo()
            {
                Console.WriteLine("Type of Account : Business Account");
                base.PrintAccountinfo();
                Console.WriteLine("the maximum for WithDraw if your Account has 0 Balance: " + Business);
            }
        }



        class Clients
        {
            protected string FullName;
            public long ID;
            protected string Job;
            protected string MobileNumber;
            protected string Address;
            public Account[] MyAccounts = new Account[2];
            public Clients(string FullName, long ID, string Job, string MobileNumber, string Address)
            {
                this.FullName = FullName;
                this.ID = ID;
                this.Job = Job;
                this.MobileNumber = MobileNumber;
                this.Address = Address;
            }
            public bool AddAccount(long Account_Number)
            {
                Console.Write("\n(1).CurrentAccount.\n(2).Business Account. \nEnter type an Account: ");
                int type = int.Parse(Console.ReadLine());
                bool DONE = false;
                if (type == 1 && MyAccounts[0] == null)
                {
                    CurrentAccount CurrentAccount1 = new CurrentAccount(Account_Number, 0);
                    MyAccounts[0] = CurrentAccount1;
                    DONE = true;
                }
                else if (type == 2 && MyAccounts[1] == null)
                {
                    BusinessAccount CurrentAccount1 = new BusinessAccount(1000000, Account_Number, 0);
                    MyAccounts[1] = CurrentAccount1;
                    DONE = true;
                }
                return DONE;
            }
            public void PrintClientInfo()
            {
                Console.WriteLine("Full Name: " + FullName);
                Console.WriteLine("ID: " + ID);
                Console.WriteLine("Job: " + Job);
                Console.WriteLine("Mobile Number: " + MobileNumber);
                if (MyAccounts[0] != null)
                {
                    Console.WriteLine("____________1____________");
                    MyAccounts[0].PrintAccountinfo();
                }
                if (MyAccounts[1] != null)
                {
                    Console.WriteLine("____________2____________");
                    MyAccounts[1].PrintAccountinfo();
                }
            }
        }



        static class BANKINGSYSTEM
        {
            private static long Account_Number = 2020101000;
            private static long ID = 20201000;
            private static ArrayList data_Clients = new ArrayList();
            public static void Add_Client()
            {
                Console.WriteLine("       Add Client\n       ---------");
                Console.Write("Enter your Full Name: ");
                string FullName = Console.ReadLine();
                Console.Write("Enter your Job: ");
                string Job = Console.ReadLine();
                Console.Write("Enter your Mobile Number: ");
                string MobileNumber = Console.ReadLine();
                Console.Write("Enter your Address: ");
                string Address = Console.ReadLine();
                Clients Clientl = new Clients(FullName, ID, Job, MobileNumber, Address);
                bool isdone = Clientl.AddAccount(Account_Number);
                if (isdone)
                {
                    data_Clients.Add(Clientl);
                    Account_Number++;
                    ID++;
                    Clientl.PrintClientInfo();
                    Console.WriteLine("\n Add Client Done.");
                }
                else
                {
                    Console.WriteLine("The operation not executed... Try Again!");
                }
                Console.WriteLine("\n\n====================================================================\n");
            }
            public static int[] Search_for_Client_Account(long Account_Number)
            {
                int[] indexes = { -1, -1 };
                int find = -1;
                for (int i = 0; i < data_Clients.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (((Clients)data_Clients[i]).MyAccounts[j] != null && ((Clients)data_Clients[i]).MyAccounts[j].Account_Number == Account_Number)
                        {
                            find = 1;
                            indexes[0] = i;
                            indexes[1] = j;
                            break;
                        }
                    }
                    if (find == 1)
                    {
                        break;
                    }
                }
                return indexes;
            }
            public static int search_For_Client(long Client_ID)
            {
                int index = -1;
                for (int i = 0; i < data_Clients.Count; i++)
                {
                    if (((Clients)data_Clients[i]).ID == Client_ID)
                    {
                        index = i;
                        ((Clients)data_Clients[i]).PrintClientInfo();
                        break;
                    }
                }
                return index;
            }
            public static void Search_For_Client()
            {
                Console.WriteLine("       Search For Client\n       -----------------");
                Console.Write("Enter your ID: ");
                long Client_ID = long.Parse(Console.ReadLine());
                int index = search_For_Client(Client_ID);
                if (index == -1)
                {
                    Console.WriteLine("The operation not executed... Try Again!");
                }
                else
                {
                    ((Clients)data_Clients[index]).PrintClientInfo();
                }
                Console.WriteLine("\n\n====================================================================\n");
            }
            public static void Open_New_Account()
            {
                Console.WriteLine("       Open New Account\n       ----------------");
                Console.Write("Enter your ID: ");
                long Client_ID = long.Parse(Console.ReadLine());
                int index_theClient = search_For_Client(Client_ID);
                if (index_theClient != -1)
                {
                    bool done = ((Clients)data_Clients[index_theClient]).AddAccount(Account_Number);
                    if (done != false)
                    {
                        ((Clients)data_Clients[index_theClient]).PrintClientInfo();
                        Account_Number++;
                        Console.WriteLine(" Open New Account Done.");
                    }
                }
                else
                {
                    Console.WriteLine("The operation not executed... Try Again!");
                }
                Console.WriteLine("\n\n====================================================================\n");
            }
            public static void Search_For_Account()
            {
                Console.WriteLine("       Search For Account\n       ------------------");
                Console.Write("Enter your Account Number : ");
                long AccountNumber = long.Parse(Console.ReadLine());
                int[] index_Client_Account = Search_for_Client_Account(AccountNumber);
                if (index_Client_Account.Contains(-1) == false)
                {
                    int index_Client = index_Client_Account[0];
                    int index_Account = index_Client_Account[1];
                    ((Clients)data_Clients[index_Client]).MyAccounts[index_Account].PrintAccountinfo();
                    Console.WriteLine("The operation is Done.");
                }
                else
                {
                    Console.WriteLine("The operation not executed... Try Again!!");
                }
                Console.WriteLine("\n\n====================================================================\n");
            }
            public static void Deposit_Money()
            {
                Console.WriteLine("     Deposit Money\n     -------------");
                Console.Write("Enter your Account Number : ");
                long AccountNumber = long.Parse(Console.ReadLine());
                int[] index_Client_Account = Search_for_Client_Account(AccountNumber);
                if (index_Client_Account.Contains(-1) == false)
                {
                    Console.Write("enter your Amount : ");
                    decimal Amount = decimal.Parse(Console.ReadLine());
                    int index_Client = index_Client_Account[0];
                    int index_Account = index_Client_Account[1];
                    ((Clients)data_Clients[index_Client]).MyAccounts[index_Account].Deposit(Amount);
                    Console.WriteLine("The operation is Done.");
                }
                else
                {
                    Console.WriteLine("The operation not executed... Try Again!!");
                }
                Console.WriteLine("\n\n====================================================================\n");
            }
            public static void Withdraw_Money()
            {
                Console.WriteLine("     Withdraw Money\n     --------------");
                Console.Write("Enter your Account Number : ");
                long AccountNumber = long.Parse(Console.ReadLine());
                int[] index_Client_Account = Search_for_Client_Account(AccountNumber);
                if (index_Client_Account.Contains(-1) == false)
                {
                    Console.Write("enter your Amount : ");
                    decimal Amount = decimal.Parse(Console.ReadLine());
                    int index_Client = index_Client_Account[0];
                    int index_Account = index_Client_Account[1];
                    ((Clients)data_Clients[index_Client]).MyAccounts[index_Account].WIthDraw(Amount);
                    Console.WriteLine("The operation is Done.");
                }
                else
                {
                    Console.WriteLine("The operation not executed... Try Again!!");
                }
                Console.WriteLine("\n\n====================================================================\n");
            }
            public static void Transfer_Money()
            {
                Console.WriteLine("    Transfer Money\n    -------------");
                Console.Write("Enter your Account Number : ");
                long AccountNumber = long.Parse(Console.ReadLine());
                Console.Write("Enter Receiver Account Number : ");
                long Receiver_AccountNumber = long.Parse(Console.ReadLine());
                int[] index_sender_Client_Account = Search_for_Client_Account(AccountNumber);
                int[] index_Receiver_Client_Account = Search_for_Client_Account(Receiver_AccountNumber);
                if (index_sender_Client_Account.Contains(-1) == false && index_Receiver_Client_Account.Contains(-1) == false && index_sender_Client_Account[1] == index_Receiver_Client_Account[1])
                {
                    Console.Write("enter your Amount : ");
                    decimal Amount = decimal.Parse(Console.ReadLine());
                    int index_sender_Client = index_sender_Client_Account[0];
                    int index_sender_Account = index_sender_Client_Account[1];
                    int index_Receiver_Client = index_Receiver_Client_Account[0];
                    int index_Receiver_Account = index_Receiver_Client_Account[1];
                    ((Clients)data_Clients[index_sender_Client]).MyAccounts[index_sender_Account].Transfer(data_Clients, index_sender_Client, index_Receiver_Client, index_sender_Account, Amount);
                    Console.WriteLine("The operation is Done.");
                }
                else { Console.WriteLine("The operation not executed...Try Again!!"); }
                Console.WriteLine("\n\n====================================================================\n");
            }
            public static void Print_info_all_Clients()
            {
                Console.WriteLine("Display List Of Clients\n-----------------------");
                for (int i = 0; i < data_Clients.Count; i++)
                {
                    ((Clients)data_Clients[i]).PrintClientInfo();
                    Console.WriteLine("-----------------------");
                }
                Console.WriteLine("\n\n====================================================================\n");
            }
            public static void Print_info_all_Accounts()
            {
                Console.WriteLine("Display List Of Accounts\n------------------------");
                for (int i = 0; i < data_Clients.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (((Clients)data_Clients[i]).MyAccounts[j] == null)
                        {
                            continue;
                        }
                        else
                        {
                            ((Clients)data_Clients[i]).MyAccounts[j].PrintAccountinfo();
                        }
                        Console.WriteLine("-------------------");
                    }
                }
                Console.WriteLine("\n\n====================================================================\n");
            }
            public static void Print_info_Accounts_by_spe_date()
            {
                Console.WriteLine("Print info Accounts by spe date\n-----------------------------");
                Console.Write("Enter The Date Like This Format>> Day/Month/Year : ");
                string Date_Accounts = Console.ReadLine();
                for (int i = 0; i < data_Clients.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Console.WriteLine("-------------------");
                        if (((Clients)data_Clients[i]).MyAccounts[j] == null)
                        {
                            continue;
                        }
                        else if (((Clients)data_Clients[i]).MyAccounts[j].Open_Date == Date_Accounts)
                        {
                            ((Clients)data_Clients[i]).MyAccounts[j].PrintAccountinfo();
                        }
                    }
                }
                Console.WriteLine("\n\n====================================================================\n");
            }
            public static void Close_Account()
            {
                Console.WriteLine("Close Account\n-------------");
                Console.Write("Enter your Account Number : ");
                long AccountNumber = long.Parse(Console.ReadLine());
                int[] index_Client_Account = Search_for_Client_Account(AccountNumber);
                if (index_Client_Account.Contains(-1) == false)
                {
                    int index_Client = index_Client_Account[0];
                    int index_Account = index_Client_Account[1];
                    if ((((Clients)data_Clients[index_Client]).MyAccounts[0] == null || ((Clients)data_Clients[index_Client]).MyAccounts[1] == null))
                    {
                        data_Clients.RemoveAt(index_Client);
                    }
                    else
                    {
                        ((Clients)data_Clients[index_Client]).MyAccounts[index_Account] = null;
                    }
                    Console.WriteLine("The operation is Done.");
                }
                else
                {
                    Console.WriteLine("The operation not executed...Try Again!!");
                }
                Console.WriteLine("\n\n====================================================================\n");
            }
        }



        static void Main(string[] args)
        {
            for (; ; )
            {
                Console.WriteLine("******** WELCOME TO OUR BANKING SYSTEM ********\n");
                Console.WriteLine("         (1).Add Client  \n         (2).Open New Account  \n         (3).Deposit Money   \n         (4).Withdraw Money   \n         (5).Transfer Money   \n         (6).Search For Client   \n         (7).Search For Account   \n         (8).Display List Of Clients   \n         (9).Display List Of Accounts   \n         (10).Print info Accounts by spe date \n         (11).Close Account  \n         (12).Exit \n");
                Console.Write("Ener Num : ");
                int num = int.Parse(Console.ReadLine());
                if (num == 1) { BANKINGSYSTEM.Add_Client(); }
                else if (num == 2) { BANKINGSYSTEM.Open_New_Account(); }
                else if (num == 3) { BANKINGSYSTEM.Deposit_Money(); }
                else if (num == 4) { BANKINGSYSTEM.Withdraw_Money(); }
                else if (num == 5) { BANKINGSYSTEM.Transfer_Money(); }
                else if (num == 6) { BANKINGSYSTEM.Search_For_Client(); }
                else if (num == 7) { BANKINGSYSTEM.Search_For_Account(); }
                else if (num == 8) { BANKINGSYSTEM.Print_info_all_Clients(); }
                else if (num == 9) { BANKINGSYSTEM.Print_info_all_Accounts(); }
                else if (num == 10) { BANKINGSYSTEM.Print_info_Accounts_by_spe_date(); }
                else if (num == 11) { BANKINGSYSTEM.Close_Account(); }
                else { break; }
            }
        }
    }
}
