using System;
using System.ComponentModel.DataAnnotations;
using WebShoes.Domain.Entities.Interfaces;

namespace WebShoes.Domain.Entities.Abstract
{
    public abstract class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            Key = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        [Key]
        public long Id { get; set; }
        
        public Guid Key { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
