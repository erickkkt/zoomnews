using System;
using System.Threading.Tasks;
using Zoomnews.Database.IRepositories;

namespace Zoomnews.Services
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IArticleRepository ArticleRepository { get; }
        ICommentRepository CommentRepository { get; }
        IMediaRepository MediaRepository { get; }
        
        int Commit();
        Task<int> CommitAsync();
    }
}
