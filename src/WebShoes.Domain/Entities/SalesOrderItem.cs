using System.ComponentModel.DataAnnotations.Schema;
using WebShoes.Domain.Entities.Abstract;

namespace WebShoes.Domain.Entities
{
    [Table("SalesOrderItem")]
    public class SalesOrderItem : BaseEntity
    {
        public long SalesOrderId { get; set; }
        public long ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public int UnitPrice { get; set; }        
    }
}
