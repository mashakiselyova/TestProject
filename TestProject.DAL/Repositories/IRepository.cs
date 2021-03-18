using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task Create(T t);
        Task Update(T t);
        Task Delete(int id);
        Task<T> FindById(int id);
        Task<List<T>> Get();
        List<T> Get(Func<T, bool> predicate);
    }
}
