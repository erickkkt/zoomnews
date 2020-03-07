using System;
using System.Collections.Generic;
using System.Text;
using Zoomnews.Database.Data;
using Zoomnews.Database.IRepositories;
using Zoomnews.Database.Models;

namespace Zoomnews.Database.Repositories
{
    public class CommentRepository : GenericRepository<Comment, ZoomnewsDbContext>, ICommentRepository
    {
        public CommentRepository(ZoomnewsDbContext context) : base(context)
        {
        }
    }
}
