using Maple.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maple.Data
{
    public sealed class PlaylistConfiguration : IEntityTypeConfiguration<PlaylistModel>
    {
        public void Configure(EntityTypeBuilder<PlaylistModel> builder)
        {
            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(t => t.Description)
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
