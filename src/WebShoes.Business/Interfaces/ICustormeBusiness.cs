using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Business.Interfaces
{
    public interface ICustormeBusiness        
    {
        bool Insert(Customer customer);

        List<Customer> Select();

        Customer GetByCpf(string cpf);

        Customer Select(long id);
    }
}
