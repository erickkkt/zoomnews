using Zoomnews.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zoomnews.Services.IServices
{
    public interface IMediaService
    {
        Task<int> CountTotalRecords();
        Task<Guid> CreateMedia(Media media);
        Task<Media> GetMedia(Guid mediaId);
        Task<Media> UpdateMedia(Media media);
        Task<bool> DeleteMedia(Guid mediaId);
        Task<IReadOnlyCollection<Media>> GetAllMedias();
        Task<IReadOnlyCollection<Media>> GetAllMedias(string sortField, string sortDirection = "asc", int pageNumber = 1, int pageSize = 5);

    }
}
