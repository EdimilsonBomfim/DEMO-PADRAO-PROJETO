using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Business.Business;
using WebShoes.Business.Interfaces;
using WebShoes.Domain;
using WebShoes.Services.Interfaces;

namespace WebShoes.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustormeBusiness _customerBusiness;

        public CustomerService(ICustormeBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        public Customer GetByCpf(string cpf)
        {
           return _customerBusiness.GetByCpf(cpf);
        }

        public bool Insert(string cPF, string email, string name)
        {
            var customer = new Customer();

            customer.CPF = cPF;
            customer.Email = email;
            customer.Name = name;

            return _customerBusiness.Insert(customer);
            
        }

        public List<Customer> Select()
        {
            return _customerBusiness.Select();
        }

    }
}
