using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maple.Domain
{
    public interface IPlaylistRepository : IRepository<PlaylistModel, int>
    {
        Task<IEnumerable<PlaylistModel>> ReadAsync();
    }
}
