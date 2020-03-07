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
    public class MediaService : IMediaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MediaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateMedia(Media media)
        {
            var createdMedia = await _unitOfWork.MediaRepository.CreateAsync(media);

            if (createdMedia != null)
                return createdMedia.Id;

            return Guid.Empty;
        }

        public async Task<IReadOnlyCollection<Media>> GetAllMedias()
        {
            return await _unitOfWork.MediaRepository.GetAllAsync();
        }

        public async Task<IReadOnlyCollection<Media>> GetAllMedias(string sortField, string sortDirection = "asc", int pageNumber = 1, int pageSize = 5)
        {
            return await _unitOfWork.MediaRepository.GetAllAsync(sortField, sortDirection, pageNumber, pageSize);
        }

        public async Task<int> CountTotalRecords()
        {
            return await _unitOfWork.MediaRepository.CountTotalRecordsAsync();
        }

        public async Task<Media> GetMedia(Guid mediaId)
        {
            return await _unitOfWork.MediaRepository.GetByIdAsync(mediaId);
        }

        public async Task<Media> UpdateMedia(Media media)
        {
            var result = await _unitOfWork.MediaRepository.UpdateAsync(media);
            return result;
        }

        public async Task<bool> DeleteMedia(Guid mediaId)
        {
            var media = await _unitOfWork.MediaRepository.GetByIdAsync(mediaId);
            return await _unitOfWork.MediaRepository.DeleteAsync(media);
        }
    }
}
