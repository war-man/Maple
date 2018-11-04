using System.Collections.Generic;
using System.Threading.Tasks;

using Maple.Domain;

namespace Maple.Data
{
    public sealed class MediaPlayerRepository : Repository<MediaPlayerModel, int>, IMediaPlayerRepository
    {
        public MediaPlayerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Task<IReadOnlyCollection<MediaPlayerModel>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<MediaPlayerModel> GetByIdAsync(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<MediaPlayerModel> GetMainMediaPlayerAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyCollection<MediaPlayerModel>> GetOptionalMediaPlayersAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
