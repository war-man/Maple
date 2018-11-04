using System;
using System.Threading.Tasks;
using Maple.Domain;
using Microsoft.EntityFrameworkCore;

namespace Maple.Data
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        internal DbContext Context { get; }

        protected UnitOfWork(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public abstract void Dispose();

        public Task SaveChanges()
        {
            return Context.SaveChangesAsync();
        }
    }
}
