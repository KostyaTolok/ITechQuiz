using Microsoft.EntityFrameworkCore;
using Domain.Entities.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class AssignRequestsConfiguration : IEntityTypeConfiguration<AssignRequest>
    {
        public void Configure(EntityTypeBuilder<AssignRequest> builder)
        {
            builder.ToTable("AssignRequests");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(s => s.CreatedDate).HasColumnType("date").IsRequired();
            builder.HasOne(a => a.User).WithMany(u => u.AssignRequests)
                .HasForeignKey(a => a.UserId);
            builder.Property(a => a.UserRole).IsRequired();
        }
    }
}