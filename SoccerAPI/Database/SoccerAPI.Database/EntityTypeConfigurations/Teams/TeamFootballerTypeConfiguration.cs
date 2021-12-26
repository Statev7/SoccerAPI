namespace SoccerAPI.Database.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SoccerAPI.Database.Models.Teams;

    public class TeamFootballerTypeConfiguration : IEntityTypeConfiguration<TeamFootballerMapping>
    {
        public void Configure(EntityTypeBuilder<TeamFootballerMapping> builder)
        {
            builder
                .HasOne(tfm => tfm.Team)
                .WithMany(t => t.Footballers)
                .HasForeignKey(tcm => tcm.TeamId);
        }
    }
}
