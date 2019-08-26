using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShoes.API.ViewModel
{
    public class CreditCardViewModel
    {
        public string Reference { get; set; }
        public int AmountInCents { get; set; }
        public string Branch { get; set; }
        public string Numer { get; set; }
        public string ExpirationDate{ get; set; }
        public string SecurityDate{ get; set; }
        public string HolderName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
