using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShoes.Domain.Entities.Abstract;

namespace WebShoes.Domain.Entities
{
    [Table("PaymentSlip")]
    public class PaymentSlip:BaseEntity
    {
        public DateTime DueDate { get; set; }
        public string BarCode { get; set; }
        public int Status { get; set; }
        public int TransactionId { get; set; }
        public string TransactionKey { get; set; }
        public long SalesOrderId { get; set; }
}
}
