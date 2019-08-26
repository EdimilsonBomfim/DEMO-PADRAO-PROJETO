using System.Collections.Generic;

namespace WebShoes.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        List<T> Select();
        T Select(long id);
        bool Insert(T obj);
        bool Update(T obj);
        
    }
}