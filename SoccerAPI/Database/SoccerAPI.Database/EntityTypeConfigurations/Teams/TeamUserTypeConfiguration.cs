namespace SoccerAPI.Database.EntityTypeConfigurations.Teams
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SoccerAPI.Database.Models.Teams;

    public class TeamUserTypeConfiguration : IEntityTypeConfiguration<TeamUserMapping>
    {
        public void Configure(EntityTypeBuilder<TeamUserMapping> builder)
        {
            builder
                .HasOne(tum => tum.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(tum => tum.TeamId);

            builder
                .HasOne(tum => tum.User)
                .WithMany(u => u.Teams)
                .HasForeignKey(tum => tum.UserId);
        }
    }
}
