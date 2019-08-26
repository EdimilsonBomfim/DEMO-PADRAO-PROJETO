using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShoes.API.ViewModel
{
    public class PaymentSlipViewModel
    {
        public String Reference { get; set; }
        public int AmounteInCents { get; set; }
        public DateTime DateTime { get; set; }
    }
}
