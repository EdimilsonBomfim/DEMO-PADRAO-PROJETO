using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain;

namespace WebShoes.Repository.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Customer GetByCpf(string cpf);
        
    }
}
