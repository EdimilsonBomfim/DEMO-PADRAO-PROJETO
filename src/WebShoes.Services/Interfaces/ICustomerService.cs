using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Domain;

namespace WebShoes.Services.Interfaces
{
    public interface ICustomerService
    {
        bool Insert(string cPF, string email, string name);

        List<Customer> Select();

        Customer GetByCpf(string cpf);
    }
}
