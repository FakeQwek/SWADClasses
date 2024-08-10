using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment_Classes
{
    internal class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public bool IsSuccessful { get; set; }

        public Payment(int id, double amount)
        {
            Id = id;
            Amount = amount;
        }

        public void ProcessPayment(PaymentCompany paymentCompany)
        {
            paymentCompany.Process(this);
        }

        public static Payment ExecuteMakePayment(int id, double amount)
        {
            Payment payment = new Payment(id, amount);
            PaymentCompany paymentCompany = new PaymentCompany("DefaultCompany");
            payment.ProcessPayment(paymentCompany);

            if (payment.IsSuccessful)
            {
                Console.WriteLine("Payment was successful.");
            }
            else
            {
                Console.WriteLine("Payment failed.");
            }

            return payment;
        }
    }
}
