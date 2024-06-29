using System.Threading.Tasks;
using library_manager_avalonia.Models;
using library_manager_avalonia.Database;

namespace library_manager_avalonia.Services
{
    public interface ICategoryService
    {
        public Task AddCategory(Category category);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategory(Category category)
        {
            await _categoryRepository.AddAsync(category);
        }
    }
}
