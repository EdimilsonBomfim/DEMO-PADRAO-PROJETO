using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShoes.Domain.Entities.Abstract;

namespace WebShoes.Domain.Entities
{
    [Table("CreditCard")]
    public class CreditCard : BaseEntity
    {
        public String Reference { get; set; }
        public int AmountInCents { get; set; }
        public DateTime AuthorizedAt { get; set; }
        public long SalesOrderId { get; set; }
    }
}
