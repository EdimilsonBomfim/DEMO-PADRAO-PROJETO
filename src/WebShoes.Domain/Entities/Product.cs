using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShoes.Domain.Entities.Abstract;

namespace WebShoes.Domain.Entities
{
    [Table("Product")]
    public class Product : BaseEntity
    { 
        [Required]
        [MaxLength(13)]
        [MinLength(1)]
        public string Code { get; set; }
        [Required]
        [MaxLength(40)]
        [MinLength(2)]
        public string Description { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(1)]
        public int Quantity { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(1)]
        public int UnitPrice { get; set; }
    }
}
