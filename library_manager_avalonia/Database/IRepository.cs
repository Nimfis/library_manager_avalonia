using System.Threading.Tasks;
using System.Collections.Generic;
using library_manager_avalonia.Models;

namespace library_manager_avalonia.Database
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
