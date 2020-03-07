using System;
using System.Collections.Generic;
using System.Text;
using Zoomnews.Database.Data;
using Zoomnews.Database.IRepositories;
using Zoomnews.Database.Models;

namespace Zoomnews.Database.Repositories
{
    public class ArticleRepository : GenericRepository<Article, ZoomnewsDbContext>, IArticleRepository
    {
        public ArticleRepository(ZoomnewsDbContext context) : base(context)
        {
        }
    }
}
