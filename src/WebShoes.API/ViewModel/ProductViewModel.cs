using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShoes.API.ViewModel
{
    /// <summary>
    /// View Model da Classe Produtos.
    /// </summary>
    public class ProductViewModel
    {

        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
}
