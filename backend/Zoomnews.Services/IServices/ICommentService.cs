using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoomnews.Database.Models;

namespace Zoomnews.Services.IServices
{
    public interface ICommentService
    {
        Task<int> CountTotalRecords();
        Task<Guid> CreateComment(Comment comment);
        Task<Comment> GetComment(Guid commentId);
        Task<Comment> UpdateComment(Comment comment);
        Task<bool> DeleteComment(Guid commentId);
        Task<IReadOnlyCollection<Comment>> GetAllComments();
        Task<IReadOnlyCollection<Comment>> GetAllComments(string sortField, string sortDirection = "asc", int pageNumber = 1, int pageSize = 5);
    }
}
