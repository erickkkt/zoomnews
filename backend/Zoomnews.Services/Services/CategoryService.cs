using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zoomnews.Database.Models;
using Zoomnews.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Zoomnews.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateCategory(Category category)
        {
            var createdCategory = await _unitOfWork.CategoryRepository.CreateAsync(category);

            if (createdCategory != null)
                return createdCategory.Id;

            return Guid.Empty;
        }

        public async Task<IReadOnlyCollection<Category>> GetAllCategories()
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<IReadOnlyCollection<Category>> GetAllCategories(string sortField, string sortDirection = "asc", int pageNumber = 1, int pageSize = 5)
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync(sortField, sortDirection, pageNumber, pageSize);
        }

        public async Task<int> CountTotalRecords()
        {
            return await _unitOfWork.CategoryRepository.CountTotalRecordsAsync();
        }

        public async Task<Category> GetCategory(Guid categoryId)
        {
            return await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var result = await _unitOfWork.CategoryRepository.UpdateAsync(category);
            return result;
        }

        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
            return await _unitOfWork.CategoryRepository.DeleteAsync(category);
        }
    }
}
