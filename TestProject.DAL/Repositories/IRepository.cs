using System.Threading.Tasks;

namespace TestProject.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task CreateAsync(T t);
        Task UpdateAsync(T t);
    }
}
