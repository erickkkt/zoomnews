using System.Threading.Tasks;
using Zoomnews.Database.Data;
using Zoomnews.Database.IRepositories;
using Zoomnews.Database.Repositories;

namespace Zoomnews.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private ZoomnewsDbContext _context;

        public ICategoryRepository CategoryRepository { get; private set; }
        public IArticleRepository ArticleRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }
        public IMediaRepository MediaRepository { get; private set; }
        

        public UnitOfWork(ZoomnewsDbContext context)
        {
            _context = context;

            CategoryRepository = new CategoryRepository(_context);
            ArticleRepository = new ArticleRepository(_context);
            CommentRepository = new CommentRepository(_context);
            MediaRepository = new MediaRepository(_context);
           
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    
        public void Dispose()
        {
        }
    }
}
