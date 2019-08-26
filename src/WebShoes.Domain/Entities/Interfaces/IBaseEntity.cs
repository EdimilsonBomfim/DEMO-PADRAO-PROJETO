using System;

namespace WebShoes.Domain.Entities.Interfaces
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        Guid Key { get; set; }
    }
}
