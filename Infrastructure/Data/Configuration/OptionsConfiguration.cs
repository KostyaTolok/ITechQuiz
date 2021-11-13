using Domain.Entities.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class OptionsConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.ToTable("Options");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(o => o.Title).HasMaxLength(100).IsRequired();
            builder.Property(o => o.IsCorrect).IsRequired();
            builder.Property(o => o.Subtitle).HasMaxLength(150);
            builder.HasOne(o => o.Question).WithMany(q => q.Options)
                .HasForeignKey(o=>o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
