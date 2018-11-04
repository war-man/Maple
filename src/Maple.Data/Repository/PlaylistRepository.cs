using System.Collections.Generic;
using System.Threading.Tasks;
using Maple.Domain;

namespace Maple.Data
{
    public sealed class PlaylistRepository : Repository<PlaylistModel, int>, IPlaylistRepository
    {
        public PlaylistRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Task<IReadOnlyCollection<PlaylistModel>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PlaylistModel> GetByIdAsync(int Id)
        {
            throw new System.NotImplementedException();
        }
    }
}
