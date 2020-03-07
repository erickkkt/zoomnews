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
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateArticle(Article article)
        {
            var createdArticle = await _unitOfWork.ArticleRepository.CreateAsync(article);

            if (createdArticle != null)
                return createdArticle.Id;

            return Guid.Empty;
        }

        public async Task<IReadOnlyCollection<Article>> GetAllArticles()
        {
            return await _unitOfWork.ArticleRepository.GetAllAsync();
        }

        public async Task<IReadOnlyCollection<Article>> GetAllRemovedArticles()
        {
            return await _unitOfWork.ArticleRepository.GetManyAsync(x => x.IsDeleted == true);
        }

        public async Task<IReadOnlyCollection<Article>> GetAllArticles(string sortField, string sortDirection = "asc", int pageNumber = 1, int pageSize = 5)
        {
            return await _unitOfWork.ArticleRepository.GetAllAsync(sortField, sortDirection, pageNumber, pageSize);
        }

        public async Task<int> CountTotalRecords()
        {
            return await _unitOfWork.ArticleRepository.CountTotalRecordsAsync();
        }

        public async Task<Article> GetArticle(Guid articleId)
        {
            return await _unitOfWork.ArticleRepository.GetByIdAsync(articleId);
        }

        public async Task<Article> UpdateArticle(Article article)
        {
            var result = await _unitOfWork.ArticleRepository.UpdateAsync(article);
            return result;
        }

        public async Task<bool> DeleteArticle(Guid articleId)
        {
            var article = await _unitOfWork.ArticleRepository.GetByIdAsync(articleId);
            return await _unitOfWork.ArticleRepository.DeleteAsync(article);
        }
    }
}
