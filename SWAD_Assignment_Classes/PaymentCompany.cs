using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment_Classes
{
    internal class PaymentCompany
    {
        public string Name { get; set; }

        public PaymentCompany(string name)
        {
            Name = name;
        }

        public virtual void Process(Payment payment)
        {
            payment.IsSuccessful = true;
        }
    }

    internal class CreditCardCompany : PaymentCompany
    {
        public CreditCardCompany(string name) : base(name) { }

        public override void Process(Payment payment)
        {
            Console.WriteLine("Processing payment through credit card...");
            payment.IsSuccessful = true;
        }
    }

    internal class DebitCardCompany : PaymentCompany
    {
        public DebitCardCompany(string name) : base(name) { }

        public override void Process(Payment payment)
        {
            Console.WriteLine("Processing payment through debit card...");
            payment.IsSuccessful = true;
        }
    }

    internal class DigitalWalletCompany : PaymentCompany
    {
        public DigitalWalletCompany(string name) : base(name) { }

        public override void Process(Payment payment)
        {
            Console.WriteLine("Processing payment through digital wallet...");
            payment.IsSuccessful = true;
        }
    }
}
