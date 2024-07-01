using System.Threading.Tasks;
using System.Collections.Generic;
using library_manager_avalonia.Models;
using System.Linq.Expressions;
using System;

namespace library_manager_avalonia.Database
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByPropertyAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        Task<IEnumerable<Book>> GetBooksWithAuthorsAndCategoriesAsync();
        Task<IEnumerable<Rental>> GetRentalsWithBooks();
    }
}
