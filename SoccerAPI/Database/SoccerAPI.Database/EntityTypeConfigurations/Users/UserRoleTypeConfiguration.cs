namespace SoccerAPI.Database.EntityTypeConfigurations.Users
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SoccerAPI.Database.Models.Users;

    public class UserRoleTypeConfiguration : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder
                .HasOne(urm => urm.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(urm => urm.UserId);

            builder
                .HasOne(urm => urm.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(urm => urm.RoleId);
        }
    }
}
