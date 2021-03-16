using System.Threading.Tasks;

namespace TestProject.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task Create(T t);
        Task Update(T t);
        Task Delete(int id);
        Task<T> Get(int id);
    }
}
