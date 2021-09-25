using Domain.Entities.Surveys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.ToTable("Surveys");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(s => s.Name).HasMaxLength(50).IsRequired();
            builder.Property(s => s.Title).HasMaxLength(50).IsRequired();
            builder.Property(s => s.Title).IsRequired();
            builder.Property(s => s.Subtitle).HasMaxLength(150);
            builder.Property(s => s.CreatedDate).HasColumnType("date").IsRequired();
            builder.HasMany(s => s.Questions).WithOne(q => q.Survey);
            builder.HasOne(s => s.User).WithMany(u => u.Surveys);
        }
    }
}
