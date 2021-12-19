using Domain.Entities.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class CategoriesConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(c => c.Title).IsRequired();
            builder.HasMany(c => c.Surveys).WithMany(s => s.Categories);
        }
    }
}