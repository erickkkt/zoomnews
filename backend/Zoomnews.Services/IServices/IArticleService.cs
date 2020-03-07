using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoomnews.Database.Models;

namespace Zoomnews.Services.IServices
{
    public interface IArticleService
    {
        Task<int> CountTotalRecords();
        Task<Guid> CreateArticle(Article article);
        Task<Article> GetArticle(Guid articleId);
        Task<Article> UpdateArticle(Article article);
        Task<bool> DeleteArticle(Guid articleId);
        Task<IReadOnlyCollection<Article>> GetAllArticles();
        Task<IReadOnlyCollection<Article>> GetAllRemovedArticles();
        Task<IReadOnlyCollection<Article>> GetAllArticles(string sortField, string sortDirection = "asc", int pageNumber = 1, int pageSize = 5);        
    }
}
