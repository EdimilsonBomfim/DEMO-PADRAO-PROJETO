using System;
using System.Collections.Generic;
using System.Text;
using WebShoes.Business.Interfaces;
using WebShoes.Domain;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Business.Business
{
    public class CustomerBusiness : ICustormeBusiness
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerBusiness(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer GetByCpf(string cpf)
        {
            return _customerRepository.GetByCpf(cpf);
        }

        public bool Insert(Customer customer)
        {
            return _customerRepository.Insert(customer);
        }

        public List<Customer> Select()
        {
            return _customerRepository.Select();
        }

        public Customer Select(long id)
        {
            return _customerRepository.Select(id);
        }
    }
}
