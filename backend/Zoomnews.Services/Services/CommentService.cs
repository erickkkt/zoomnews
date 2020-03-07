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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateComment(Comment comment)
        {
            var createdComment = await _unitOfWork.CommentRepository.CreateAsync(comment);

            if (createdComment != null)
                return createdComment.Id;

            return Guid.Empty;
        }

        public async Task<IReadOnlyCollection<Comment>> GetAllComments()
        {
            return await _unitOfWork.CommentRepository.GetAllAsync();
        }

        public async Task<IReadOnlyCollection<Comment>> GetAllComments(string sortField, string sortDirection = "asc", int pageNumber = 1, int pageSize = 5)
        {
            return await _unitOfWork.CommentRepository.GetAllAsync(sortField, sortDirection, pageNumber, pageSize);
        }

        public async Task<int> CountTotalRecords()
        {
            return await _unitOfWork.CommentRepository.CountTotalRecordsAsync();
        }

        public async Task<Comment> GetComment(Guid commentId)
        {
            return await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            var result = await _unitOfWork.CommentRepository.UpdateAsync(comment);
            return result;
        }

        public async Task<bool> DeleteComment(Guid commentId)
        {
            var Comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
            return await _unitOfWork.CommentRepository.DeleteAsync(Comment);
        }
    }
}
