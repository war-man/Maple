﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Maple.Data
{
    public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PlaylistContext>
    {
        public PlaylistContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .Build();

            var builder = new DbContextOptionsBuilder<PlaylistContext>()
                .UseSqlite(Constants.ConnectionString);

            return new PlaylistContext(builder.Options, new LoggerFactory());
        }
    }
}
