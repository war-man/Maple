﻿// <auto-generated />
using System;
using Maple.Data;
using Maple.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Maple.Data.Migrations
{
    [DbContext(typeof(PlaylistContext))]
    partial class PlaylistContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("Maple.Domain.MediaItemModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("SYSTEM");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<long>("Duration");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(2048);

                    b.Property<int>("MediaItemType");

                    b.Property<int>("PlaylistId");

                    b.Property<int>("PrivacyStatus");

                    b.Property<int>("Sequence");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("SYSTEM");

                    b.Property<DateTime>("UpdatedOn")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("PlaylistId");

                    b.ToTable("MediaItems");
                });

            modelBuilder.Entity("Maple.Domain.MediaPlayerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("SYSTEM");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeviceName")
                        .HasMaxLength(100);

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<PlaylistModel>("Playlist")
                        .IsRequired()
                        .HasColumnType("PlaylistModel");

                    b.Property<int>("PlaylistId");

                    b.Property<int>("Sequence");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("SYSTEM");

                    b.Property<DateTime>("UpdatedOn")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("Mediaplayers");
                });

            modelBuilder.Entity("Maple.Domain.OptionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("SYSTEM");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<int>("Sequence");

                    b.Property<int>("Type");

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("SYSTEM");

                    b.Property<DateTime>("UpdatedOn")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Options");

                    b.HasData(
                        new { Id = 1, CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Key = "SelectedPlaylist", Sequence = 0, Type = 1, UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Value = "1" },
                        new { Id = 2, CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Key = "SelectedMediaItem", Sequence = 10, Type = 2, UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Value = "" },
                        new { Id = 3, CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Key = "SelectedMediaPlayer", Sequence = 20, Type = 4, UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Value = "1" },
                        new { Id = 5, CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Key = "SelectedPrimary", Sequence = 40, Type = 8, UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Value = "" },
                        new { Id = 6, CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Key = "SelectedAccent", Sequence = 50, Type = 8, UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Value = "" },
                        new { Id = 7, CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Key = "SelectedScene", Sequence = 60, Type = 16, UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Value = "" }
                    );
                });

            modelBuilder.Entity("Maple.Domain.PlaylistModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("SYSTEM");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<bool>("IsShuffeling");

                    b.Property<string>("Location");

                    b.Property<int>("MediaPlayerId");

                    b.Property<int>("PrivacyStatus");

                    b.Property<int>("RepeatMode");

                    b.Property<int>("Sequence");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UpdatedBy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("SYSTEM");

                    b.Property<DateTime>("UpdatedOn")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("MediaPlayerId");

                    b.ToTable("Playlists");

                    b.HasData(
                        new { Id = 1, CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Description = "Test playlist with mp3 files", IsShuffeling = false, Location = "https://www.youtube.com/watch?v=WxfcsmbBd00&t=0s", MediaPlayerId = 1, PrivacyStatus = 0, RepeatMode = 1, Sequence = 0, Title = "MP3 Files", UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                    );
                });

            modelBuilder.Entity("Maple.Domain.MediaItemModel", b =>
                {
                    b.HasOne("Maple.Domain.PlaylistModel", "Playlist")
                        .WithMany("MediaItems")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Maple.Domain.PlaylistModel", b =>
                {
                    b.HasOne("Maple.Domain.MediaPlayerModel", "MediaPlayer")
                        .WithMany()
                        .HasForeignKey("MediaPlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}