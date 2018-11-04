using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Maple.Domain
{
    public interface IReadOnlyRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        TEntity Read(TKey key);

        Task<TEntity> ReadAsync(TKey key);

        IEnumerable<TEntity> Read(Expression<Func<TEntity, bool>> filter, string[] propertyNames, int index, int count);

        Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, string[] propertyNames, int index, int count);
    }
}
