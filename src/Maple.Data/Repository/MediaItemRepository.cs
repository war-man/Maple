using System.Collections.Generic;
using System.Threading.Tasks;

using Maple.Domain;

namespace Maple.Data
{
    public sealed class MediaItemRepository : Repository<MediaItemModel, int>, IMediaItemRepository
    {
        public MediaItemRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Task<IReadOnlyCollection<MediaItemModel>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<MediaItemModel> GetByIdAsync(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<MediaItemModel> GetMediaItemByPlaylistIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
