using Dapper;
using Npgsql;
using System;
using System.Data;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Repository.Repository
{
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCart GetActiveShoppingCart(long customerId)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId, DbType.Int64);
                return conn.QueryFirstOrDefault<ShoppingCart>("SELECT * FROM ShoppingCart WHERE CartStatus = 0 AND CustomerId = @CustomerId", parameters);
            }
        }

        public bool UpdateStatus(long shoppingCartId, ShoppingCartStatus shoppingStatus)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                using (var transaction = conn.BeginTransaction())
                {
                    conn.Open();

                    try
                    {
                        var cartStatusEnum = (byte)shoppingStatus;

                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@ShoppingCartId", shoppingCartId, DbType.Int64);
                        parameters.Add("@CartStatus", cartStatusEnum, DbType.Byte);
                        conn.QueryFirstOrDefault<SalesOrder>("UPDATE ShoppingCart SET CartStatus = @CartStatus WHERE Id = @ShoppingCartId", parameters, transaction);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
