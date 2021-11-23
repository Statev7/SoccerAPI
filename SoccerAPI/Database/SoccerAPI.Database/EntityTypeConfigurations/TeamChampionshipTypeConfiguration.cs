namespace SoccerAPI.Database.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SoccerAPI.Database.Models.Teams;

    public class TeamChampionshipTypeConfiguration : IEntityTypeConfiguration<TeamChampionshipMapping>
    {
        public void Configure(EntityTypeBuilder<TeamChampionshipMapping> builder)
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
