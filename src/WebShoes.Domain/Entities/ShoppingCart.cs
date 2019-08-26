using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShoes.Domain.Entities.Abstract;
using WebShoes.Domain.Entities.Values;

namespace WebShoes.Domain.Entities
{
    [Table("ShoppingCart")]
    public class ShoppingCart : BaseEntity
    {
        public long CustomerId { get; set; }

        public ShoppingCartStatus CartStatus { get; set; }

    }
}
