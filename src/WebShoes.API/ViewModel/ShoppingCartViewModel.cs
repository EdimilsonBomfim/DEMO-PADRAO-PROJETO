using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShoes.Domain.Entities.Values;

namespace WebShoes.API.ViewModel
{
    public class ShoppingCartViewModel
    {
        public long ShoppingCartId { get; set; }

        public ShoppingCartStatus ShoppingCartStatus { get; set; }
    }
}
