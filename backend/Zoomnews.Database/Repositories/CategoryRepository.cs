using System;
using System.Collections.Generic;
using System.Text;
using Zoomnews.Database.Data;
using Zoomnews.Database.IRepositories;
using Zoomnews.Database.Models;

namespace Zoomnews.Database.Repositories
{
    public class CategoryRepository : GenericRepository<Category, ZoomnewsDbContext>, ICategoryRepository
    {
        public CategoryRepository(ZoomnewsDbContext context) : base(context)
        {
        }
    }
}
