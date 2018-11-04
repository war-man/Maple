﻿using System;
using System.Collections.Generic;
using Maple.Domain;
using Maple.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Maple.Data
{
    public sealed class PlaylistContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public DbSet<PlaylistModel> Playlists { get; set; }
        public DbSet<MediaItemModel> MediaItems { get; set; }
        public DbSet<MediaPlayerModel> Mediaplayers { get; set; }
        public DbSet<OptionModel> Options { get; set; }

        public PlaylistContext(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        internal PlaylistContext(DbContextOptions<PlaylistContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            _loggerFactory.AddLog4Net();

            optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(_loggerFactory)
                .UseSqlite(Constants.ConnectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MediaPlayerConfiguration());
            modelBuilder.ApplyConfiguration(new MediaItemConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new OptionConfiguration());

            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            var _playlistTitles = new List<string>()
            {
                "Memories Of A Time To Come",
                "A Twist In The Myth",
                "At The Edge Of Time",
                "Beyond The Red Mirror",
                "Battalions Of Fear",
                "Follow The Blind",
                "Tales From The Twilight World",
                "Somewhere Far Beyond",
                "Tokyo Tales",
                "Imaginations From The Other Side",
                "The Forgotten Tales",
                "Nightfall In Middle Earth",
                "A Night At The Opera",
                "Infinite",
                "The Slim Shady LP",
                "Marshall Mathers",
                "The Eminem Show",
                "Encore",
                "Relapse",
                "Recovery",
            };

            var _mediaItemTitles = new List<string>()
            {
                "The Ninth Wave",
                "Twilight Of The Gods",
                "Prophecies",
                "At The Edge Of Time",
                "Ashes Of Eternity",
                "Distant Memories",
                "The Holy Grail",
                "The Throne",
                "Sacred Mind",
                "Miracle Machine",
                "Grand Parade",
                "My Name Is",
                "Guilty Conscience",
                "Brain Damage",
                "Paul",
                "If I Had",
                "'97 Bonnie & Clyde",
                "Bitch",
                "Role Model",
            };

            var playlist = new PlaylistModel
            {
                Title = "MP3 Files",
                Description = "Test playlist with mp3 files",
                Id = 1,
                IsShuffeling = false,
                Location = "https://www.youtube.com/watch?v=WxfcsmbBd00&t=0s",
                PrivacyStatus = 0,
                RepeatMode = 1,
                Sequence = 0,
                MediaPlayerId = 1,
            };

            var mediaPlayer = new MediaPlayerModel
            {
                Id = 1,
                IsPrimary = true,
                Name = "Main",
                PlaylistId = 1,
                Sequence = 0,
                DeviceName = "",
            };

            //playlist.MediaPlayer = mediaPlayer;
            //mediaPlayer.Playlist = playlist;

            modelBuilder.Entity<PlaylistModel>().HasData(playlist);
            modelBuilder.Entity<MediaPlayerModel>().HasData();

            //modelBuilder.Entity<MediaItemModel>().HasData(new MediaItemModel
            //{
            //    Title = "Universe Words",
            //    Description = "http://freemusicarchive.org/music/Artofescapism/",
            //    Duration = 60_000_000,
            //    Id = 1,
            //    Location = ".\\Resources\\Art_Of_Escapism_-_Universe_Words.mp3",
            //    PlaylistId = 0,
            //    PrivacyStatus = 0,
            //    Sequence = 0,
            //    MediaItemType = (int)MediaItemType.LocalFile,
            //});

            //for (var i = 1; i < _playlistTitles.Count; i++)
            //{
            //    modelBuilder.Entity<PlaylistModel>().HasData(new PlaylistModel
            //    {
            //        Title = _playlistTitles[i],
            //        Description = "Test playlist with 3 entries",
            //        Id = i,
            //        IsShuffeling = false,
            //        Location = "https://www.youtube.com/watch?v=WxfcsmbBd00&t=0s",
            //        PrivacyStatus = 0,
            //        RepeatMode = 1,
            //        Sequence = i,
            //    });

            //    for (var j = 2; j < _mediaItemTitles.Count; j++)
            //    {
            //        modelBuilder.Entity<MediaItemModel>().HasData(new MediaItemModel
            //        {
            //            Title = _mediaItemTitles[j],
            //            Description = "A popular youtube video",
            //            Duration = 60_000_000,
            //            Id = j,
            //            Location = "https://www.youtube.com/watch?v=oHg5SJYRHA0",
            //            PlaylistId = i,
            //            PrivacyStatus = 0,
            //            Sequence = j,
            //        });
            //    }
            //}

            modelBuilder.Entity<OptionModel>().HasData(new OptionModel
            {
                Id = 1,
                Key = "SelectedPlaylist",
                Sequence = 0,
                Type = (int)OptionType.Playlist,
                Value = "1",
            });

            modelBuilder.Entity<OptionModel>().HasData(new OptionModel
            {
                Id = 2,
                Key = "SelectedMediaItem",
                Sequence = 10,
                Type = (int)OptionType.MediaItem,
                Value = "",
            });

            modelBuilder.Entity<OptionModel>().HasData(new OptionModel
            {
                Id = 3,
                Key = "SelectedMediaPlayer",
                Sequence = 20,
                Type = (int)OptionType.MediaPlayer,
                Value = "1",
            });

            // 4

            modelBuilder.Entity<OptionModel>().HasData(new OptionModel
            {
                Id = 5,
                Key = "SelectedPrimary",
                Sequence = 40,
                Type = (int)OptionType.ColorProfile,
                Value = "",
            });

            modelBuilder.Entity<OptionModel>().HasData(new OptionModel
            {
                Id = 6,
                Key = "SelectedAccent",
                Sequence = 50,
                Type = (int)OptionType.ColorProfile,
                Value = "",
            });

            modelBuilder.Entity<OptionModel>().HasData(new OptionModel
            {
                Id = 7,
                Key = "SelectedScene",
                Sequence = 60,
                Type = (int)OptionType.Scene,
                Value = "",
            });
        }
    }
}
