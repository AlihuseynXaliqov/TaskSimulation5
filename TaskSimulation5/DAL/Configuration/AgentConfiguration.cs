using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSimulation5.Models;

namespace TaskSimulation5.DAL.Configuration
{
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);

            builder.HasOne(x=>x.Positions).WithMany(x=>x.Agents).HasForeignKey(x=>x.PositionId);

            
        }
    }
}
