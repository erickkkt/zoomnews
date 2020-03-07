using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoomnews.Database.Models;

namespace Zoomnews.Services.IServices
{
    public interface ICategoryService
    {
        Task<int> CountTotalRecords();
        Task<Guid> CreateCategory(Category category);
        Task<Category> GetCategory(Guid categoryId);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategory(Guid categoryId);
        Task<IReadOnlyCollection<Category>> GetAllCategories();
        Task<IReadOnlyCollection<Category>> GetAllCategories(string sortField, string sortDirection = "asc", int pageNumber = 1, int pageSize = 5);
    }
}
