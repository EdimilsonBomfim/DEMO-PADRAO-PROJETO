using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShoes.Domain.Entities.Values;

namespace WebShoes.API.ViewModel
{
    public class ShoppingCartItemViewModel
    {
        public long ShoppingCartId { get; set; }
        public long ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
