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
            a1.DisplayAccountInfo();
            a2.DisplayAccountInfo();
            
            Console.WriteLine();
            a1.InterestAfterMonths(12);
            a2.InterestAfterMonths(12);


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
                Console.WriteLine($"Balance: ${_balance}");
                Console.WriteLine($"Interest Rate: {_interestRate*100}%");
                //Console.WriteLine(this.GetType());
                if (this is DepositAccount) 
                {                    
                    Console.WriteLine("Deposit Account");
                }
                else if (this is LoanAccount)
                {
                    Console.WriteLine("Loan Account");
                }
                else if (this is MortgageAccount)
                {
                    Console.WriteLine("Mortgage Account");
                }
                else
                {
                    Console.WriteLine("Invalid Account!");
                }
                Console.WriteLine();
            }

            public double InterestAfterMonths(int numOfMonths)
            {
                double calculatedInterest = 0;
                if (this is DepositAccount)
                {
                    // Deposit accounts have no interest rate if their balance is positive and less than 1000.
                    Console.WriteLine($"Calculating interest for {_customer.name}'s Deposit Account after {numOfMonths} months...");
                    if (_balance > 0 && _balance <= 1000) calculatedInterest = _balance;                    
                    else calculatedInterest = _balance * Math.Pow(1 + _interestRate, numOfMonths);
                }
                else if (this is LoanAccount)
                {
                    //  Loan accounts have no interest rate during the first 3 months if held by individuals and during the first 2 months if held by a company.
                    Console.WriteLine($"Calculating interest for {_customer.name}'s Loan Account after {numOfMonths} months...");
                    int gracePeriod = (_customer.type == "individual") ? 3 : 2;
                    if (numOfMonths <= gracePeriod) calculatedInterest = _balance;                  
                    else calculatedInterest = _balance * Math.Pow(1 + _interestRate, numOfMonths - gracePeriod);
                }
                else if (this is MortgageAccount)
                {
                    // Mortgage accounts have ½ the interest rate during the first 12 months for companies and no interest rate during the first 6 months for individuals.
                    Console.WriteLine($"Calculating interest for {_customer.name}'s Mortgage Account after {numOfMonths} months...");
                    int gracePeriod = (_customer.type == "company") ? 12 : 6;
                    double adjustedRate = (_customer.type == "company") ? (_interestRate / 2) : 0;
                    calculatedInterest = (_balance * Math.Pow(1 + adjustedRate, gracePeriod)) + (_balance * Math.Pow(1 + _interestRate, numOfMonths - gracePeriod));
                }
                else
                {
                    Console.WriteLine("Cannot calculate interest for Invalid Account!");
                }
                Console.WriteLine($" -> Accumulated interest would ${calculatedInterest - _balance}.");
                Console.WriteLine($" -> New balance would be ${calculatedInterest}.\n");
                return calculatedInterest;
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

        






        //public class Person
        //{
        //    protected string Name { get; set; }

        //    public Person(string name)
        //    {
        //        Name = name;
        //    }

        //    public void SayHello()
        //    {
        //        Console.WriteLine("Hello! My name is " + Name);
        //    }
        //}

        //public class Teacher : Person
        //{
        //    public Teacher(string name) : base(name)
        //    {
        //    }

        //    public void Teach()
        //    {
        //        Console.WriteLine(Name + " teaches");
        //    }
        //}

        //public class Student : Person
        //{
        //    public Student(string name) : base(name)
        //    {
        //    }

        //    public void Study()
        //    {
        //        Console.WriteLine(Name + " studies");
        //    }
        //}
    }

}
