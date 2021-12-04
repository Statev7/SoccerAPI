namespace SoccerAPI.Database.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SoccerAPI.Database.Models.Teams;

    public class CoachTypeConfiguration : IEntityTypeConfiguration<Coach>
    {
        
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder
                .HasOne(tcm => tcm.Team)
                .WithMany(t => t.Coaches)
                .HasForeignKey(tcm => tcm.TeamId);
        }
    }
}
