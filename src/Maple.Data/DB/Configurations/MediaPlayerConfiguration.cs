using Maple.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maple.Data
{
    public sealed class MediaPlayerConfiguration : IEntityTypeConfiguration<MediaPlayerModel>
    {
        public void Configure(EntityTypeBuilder<MediaPlayerModel> builder)
        {
            builder.Property(t => t.Id)
                .HasDefaultValueSql("GetDate()");

            builder.Property(t => t.Playlist)
                   .HasColumnType("PlaylistModel")
                   .IsRequired();

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(t => t.DeviceName)
                   .HasMaxLength(100);

            builder.HasKey(t => t.Id);

            builder.Property(t => t.CreatedOn)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.UpdatedOn)
                   .IsRequired()
                   .ValueGeneratedOnAddOrUpdate();

            builder.Property(t => t.CreatedBy)
                   .HasDefaultValue("SYSTEM");

            builder.Property(t => t.UpdatedBy)
                   .HasDefaultValue("SYSTEM");
        }
    }
}
