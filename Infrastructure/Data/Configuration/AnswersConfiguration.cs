using System;
using Domain.Entities.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class AnswersConfiguration: IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd().IsRequired();
            builder.HasOne(a => a.User).WithMany(u => u.Answers)
                .HasForeignKey(a => a.UserId);
            builder.HasOne(a => a.Question).WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);
            builder.HasMany(a => a.SelectedOptions).WithMany(o => o.Answers);

        }
    }
}