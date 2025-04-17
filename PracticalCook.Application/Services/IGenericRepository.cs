using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticalCook.Application.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task<T> UpdateAsync(T entity);

        Task<T> RemoveAsync(int id);
    }
}
