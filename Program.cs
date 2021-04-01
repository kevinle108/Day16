using System;

namespace Day16
{    
    public class Inheritance
    {
        public static void Main(string[] args)
        {
            var a1 = new DepositAccount("Kevin", "individual", 1000, 0.015);
            var a2 = new LoanAccount("Dep", "individual", 30000, 0.07);
            var a3 = new MortgageAccount("IVS LLC", "company", 80000, 0.04);
            var a4 = new MortgageAccount("John", "individual", 80000, 0.04);

            a1.DisplayAccountInfo();
            a2.DisplayAccountInfo();
            a3.DisplayAccountInfo();
            a4.DisplayAccountInfo();
            
            Console.WriteLine();
            a1.InterestAfterMonths(12);
            a2.InterestAfterMonths(12);
            a3.InterestAfterMonths(12);
            a4.InterestAfterMonths(12);          
        }

        public class Account
        {
            public struct Customer
            {
                public string name;
                public string type;
            }

            protected Customer _customer;
            protected double _balance;
            protected double _interestRate;

            public Account(string customerName, string customerType, double balance, double interestRate)
            {
                _customer = new Customer { name = customerName, type = customerType };
                _balance = balance;
                _interestRate = interestRate;
            }

            public void Deposit(double amt) 
            {
                _balance += amt;
                Console.WriteLine($"${amt} has been successfully deposited into {_customer.name}'s account.");
                Console.WriteLine($" -> Updated Balance: ${_balance}");
                Console.WriteLine();
            }

            public void DisplayAccountInfo()
            {
                Console.WriteLine($"Name: {_customer.name}, {_customer.type}");
                Console.WriteLine($" Balance: ${_balance}");
                Console.WriteLine($" Interest Rate: {Math.Round(_interestRate*100, 3)}%");
                //Console.WriteLine(this.GetType());
                if (this is DepositAccount) Console.WriteLine(" Deposit Account");
                else if (this is LoanAccount) Console.WriteLine(" Loan Account");
                else if (this is MortgageAccount) Console.WriteLine(" Mortgage Account");
                else Console.WriteLine(" Invalid Account!");
                Console.WriteLine();
            }

            public double InterestAfterMonths(int numOfMonths)
            {
                double updatedBalance = 0;
                string accountType = "";
                if (this is DepositAccount)
                {
                    // Deposit accounts have no interest rate if their balance is positive and less than 1000.
                    accountType = "Deposit";
                    if (_balance > 0 && _balance <= 1000) updatedBalance = _balance;                    
                    else updatedBalance = _balance * Math.Pow(1 + _interestRate, numOfMonths);
                }
                else if (this is LoanAccount)
                {
                    //  Loan accounts have no interest rate during the first 3 months if held by individuals and during the first 2 months if held by a company.
                    accountType = "Loan";
                    int gracePeriod = (_customer.type == "individual") ? 3 : 2;
                    if (numOfMonths <= gracePeriod) updatedBalance = _balance;                  
                    else updatedBalance = _balance * Math.Pow(1 + _interestRate, numOfMonths - gracePeriod);
                }
                else if (this is MortgageAccount)
                {
                    // Mortgage accounts have ½ the interest rate during the first 12 months for companies and no interest rate during the first 6 months for individuals.
                    accountType = "Mortgage";                    
                    int gracePeriod = (_customer.type == "company") ? 12 : 6;
                    double adjustedRate = (_customer.type == "company") ? (_interestRate / 2) : 0;
                    updatedBalance = (_balance * Math.Pow(1 + adjustedRate, gracePeriod)) + (_balance * Math.Pow(1 + _interestRate, numOfMonths - gracePeriod));
                }
                else
                {
                    Console.WriteLine("Cannot calculate interest for Invalid Account!");
                }
                updatedBalance = Math.Round(updatedBalance, 2);
                Console.WriteLine($"Calculating interest for {_customer.name}'s {accountType} Account ({Math.Round(_interestRate * 100, 3)}% interest) after {numOfMonths} months...");
                Console.WriteLine($" -> Accumulated interest would ${Math.Round(updatedBalance - _balance, 2)}.");
                Console.WriteLine($" -> New balance would be ${updatedBalance}.\n");
                return updatedBalance;
            }
        }

        public class DepositAccount : Account
        {
            public DepositAccount(string customerName, string customerType, double balance, double interestRate) : base(customerName, customerType, balance, interestRate)
            {
            }

            public void Withdraw(double amt)
            {
                _balance -= amt;
                Console.WriteLine($"${amt} has been successfully withdrawn from {_customer.name}'s account.");
                Console.WriteLine($" -> Updated Balance: ${_balance}");
                Console.WriteLine();
            }
        }

        public class LoanAccount : Account
        {
            public LoanAccount(string customerName, string customerType, double balance, double interestRate) : base(customerName, customerType, balance, interestRate)
            {
            }
        }

        public class MortgageAccount : Account
        {
            public MortgageAccount(string customerName, string customerType, double balance, double interestRate) : base(customerName, customerType, balance, interestRate)
            {
            }
        }     
    }

}
