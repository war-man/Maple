using System;
using System.Threading.Tasks;

namespace Maple.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChanges();
    }
}
