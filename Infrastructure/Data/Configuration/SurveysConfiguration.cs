using Domain.Entities.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class SurveysConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.ToTable("Surveys");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(s => s.Title).HasMaxLength(50).IsRequired();
            builder.Property(s => s.Title).IsRequired();
            builder.Property(s => s.Type).IsRequired();
            builder.Property(s => s.Subtitle).HasMaxLength(150);
            builder.Property(s => s.CreatedDate).HasColumnType("date").IsRequired();
            builder.HasMany(s => s.Questions).WithOne(q => q.Survey);
            builder.HasOne(s => s.User).WithMany(u => u.Surveys)
                .HasForeignKey(s=>s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
