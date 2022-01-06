namespace SoccerAPI.Infrastructure.AutoMapperProfiles
{
    using AutoMapper;

    using SoccerAPI.Database.Models.Users;
    using SoccerAPI.DTOs.Role;

    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            this.CreateMap<Role, GetRoleFroSessionDTO>();
            this.CreateMap<Role, GetRoleIdDTO>();
        }
    }
}
