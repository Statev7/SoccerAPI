namespace SoccerAPI.Database.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SoccerAPI.Database.Models.Championships;

    public class ChampionshipTeamTypeConfiguration : IEntityTypeConfiguration<ChampionshipTeamMapping>
    {
        public void Configure(EntityTypeBuilder<ChampionshipTeamMapping> builder)
        {
            builder
                .HasOne(tcm => tcm.Team)
                .WithMany(c => c.Championships)
                .HasForeignKey(tcm => tcm.TeamId);

            builder
                .HasOne(tcm => tcm.Championship)
                .WithMany(t => t.Teams)
                .HasForeignKey(tcm => tcm.ChampionshipId);
        }
    }
}
