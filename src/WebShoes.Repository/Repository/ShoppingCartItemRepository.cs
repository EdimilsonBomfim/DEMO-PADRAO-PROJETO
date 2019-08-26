using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebShoes.Domain.Entities;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Repository.Repository
{
    public class ShoppingCartItemRepository : BaseRepository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public List<ShoppingCartItem> GetShoppingCartItems(long shoppingCartId, bool active)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ShoppingCartId", shoppingCartId, DbType.Int64);
                parameters.Add("@Active", active, DbType.Boolean);

                return conn.Query<ShoppingCartItem>("SELECT * FROM ShoppingCartItem WHERE Active = @Active AND ShoppingCartId = @ShoppingCartId", parameters).ToList();
            }
        }

        public void RemoveAll(long shoppingCartId)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ShoppingCartId", shoppingCartId, DbType.Int64);

                conn.Execute("UPDATE  ShoppingCartItem SET Active = false WHERE ShoppingCartId = @ShoppingCartId", parameters);
            }
        }

        public void RemoveProductQuantity(long shoppingCartId, long productId, int productQuantity)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ProductQuantity", shoppingCartId, DbType.Int64);
                parameters.Add("@ShoppingCartId", shoppingCartId, DbType.Int64);
                parameters.Add("@ProductId", shoppingCartId, DbType.Int64);

                conn.Execute("UPDATE  ShoppingCartItem SET ProductQuantity = ProductQuantity - @ProductQuantity   WHERE Active = true AND ShoppingCartId = @ShoppingCartId AND ProductId = @ProductId", parameters);
            }
        }
    }
}
