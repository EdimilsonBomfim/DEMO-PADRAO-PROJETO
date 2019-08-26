using Dapper;
using Npgsql;
using System;
using System.Data;
using WebShoes.Domain;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Repository.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        Customer ICustomerRepository.GetByCpf(string cpf)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@cpf", cpf, DbType.Int64);
                    return conn.QueryFirstOrDefault<Customer>("Select * from Customer Where CPF = @cpf", parameters);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    
                    return null;
                }
            }
        }       
    }
}
