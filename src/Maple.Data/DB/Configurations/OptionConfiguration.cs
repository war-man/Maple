using Maple.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maple.Data
{
    public sealed class OptionConfiguration : IEntityTypeConfiguration<OptionModel>
    {
        public void Configure(EntityTypeBuilder<OptionModel> builder)
        {
            builder.Property(t => t.Key)
                   .IsRequired();

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
