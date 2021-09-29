using Domain.Entities.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class QuestionsConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(q => q.Title).HasMaxLength(100).IsRequired();
            builder.Property(q => q.Multiple).IsRequired();
            builder.Property(q => q.MaxSelected).IsRequired();
            builder.Property(q => q.Required).IsRequired();
            builder.HasMany(q => q.Options).WithOne(q => q.Question);
            builder.HasOne(q => q.Survey).WithMany(s => s.Questions);
        }
    }
}
