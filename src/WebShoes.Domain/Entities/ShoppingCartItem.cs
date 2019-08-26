using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShoes.Domain.Entities.Abstract;

namespace WebShoes.Domain.Entities
{
    [Table("ShoppingCartItem")]
    public class ShoppingCartItem : BaseEntity
    {
        public long ShoppingCartId { get; set; }
        public long ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public bool Active { get; set; }
    }
}