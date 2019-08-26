using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebShoes.Domain.Entities.Abstract;
using WebShoes.Repository.Interfaces;

namespace WebShoes.Repository.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly string connectionString;

        public BaseRepository()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = config.GetConnectionString("DefaultConnection");            
        }

        public bool Insert(T obj)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        var result = conn.Insert<T>(obj, transaction);
                        transaction.Commit();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }                
            }
        }

        public List<T> Select()
        {
            try
            {
                //using (var conn = new SqlConnection(connectionString))
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    return conn.GetAll<T>().ToList();
                }
            }
            catch
            {
                return null;
            }

        }

        public T Select(long id)
        {
            try
            {
                //using (var conn = new SqlConnection(connectionString))
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    return conn.GetAll<T>().FirstOrDefault(x => x.Id == id);
                }
            }
            catch
            {
                return null;
            }

        }

        public bool Update(T obj)
        {
            //using (var conn = new SqlConnection(connectionString))
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Update<T>(obj);
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
