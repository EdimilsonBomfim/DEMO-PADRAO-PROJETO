using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShoes.Domain.Entities.Abstract;
using WebShoes.Domain.Entities.Interfaces;

namespace WebShoes.Domain
{
    [Table("Customer")]
    public class Customer : BaseEntity
    {
        [Required]
        [MaxLength(11)]
        [MinLength(1)]        
        public string CPF { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Email { get; set; } //



    }
}
