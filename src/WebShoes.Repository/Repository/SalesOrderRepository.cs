using Dapper;
using Dapper.Contrib.Extensions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebShoes.Domain.Entities;
using WebShoes.Domain.Entities.Values;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Repository.Repository
{
    public class SalesOrderRepository : BaseRepository<SalesOrder>, ISalesOrderRepository
    {
        public bool Add(SalesOrder salesOrder)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        var salesOrderId = conn.Insert<SalesOrder>(salesOrder, transaction);

                        foreach (var salesOderItem in salesOrder.ListSalesOrderItem)
                        {
                            conn.Insert<SalesOrderItem>(salesOderItem, transaction);
                        }                        

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

        public IEnumerable<SalesOrder> GetByDate(DateTime date)
        {
            return null;
        }

        public bool UpdateStatus(long salesOrderId, OrderStatus orderStatus)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        var orderStatusEnum = (byte)orderStatus;

                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@SalesOrderId", salesOrderId, DbType.Int64);
                        parameters.Add("@OrderStatus", orderStatusEnum, DbType.Byte);

                        var affectedRows = conn.Execute("UPDATE SalesOrder SET OrderStatus = @OrderStatus WHERE Id = @SalesOrderId", parameters, transaction);

                        if (affectedRows == 1)
                        {
                            transaction.Commit();
                            return true;
                        }
                                                
                        return false;
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

        public SalesOrder GetByShoppingCartId(long shoppingCartId)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ShoppingCartId", shoppingCartId, DbType.Int64);

                    var salesOrder = conn.QueryFirstOrDefault<SalesOrder>("SELECT * FROM SalesOrder WHERE ShoppingCartId = @ShoppingCartId", parameters);

                    if (salesOrder != null)
                    {
                        parameters.Add("@SalesOrderId", salesOrder.Id, DbType.Int64);

                        var salesOrderItem = conn.Query<SalesOrderItem>("SELECT * FROM SalesOrderItem WHERE SalesOrderId = @SalesOrderId", parameters);                        

                        if (salesOrderItem != null && salesOrderItem.Any())
                        {
                            foreach(var i in salesOrderItem)
                            {
                                salesOrder.ListSalesOrderItem.Add(i);
                            }
                        }
                    }

                    return salesOrder;
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
