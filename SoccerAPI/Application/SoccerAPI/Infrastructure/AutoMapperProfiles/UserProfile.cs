namespace SoccerAPI.Infrastructure.AutoMapperProfiles
{
    using AutoMapper;

    using SoccerAPI.Database.Models.Users;
    using SoccerAPI.DTOs.Role;
    using SoccerAPI.DTOs.User;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<PostUserLoginDTO, User>();
            this.CreateMap<PostUserRegisterDTO, User>();
            this.CreateMap<User, GetUserForSessionDTO>();
            this.CreateMap<User, GetUserInformationDTO>();

            this.CreateMap<UserRoleMapping, GetRoleFroSessionDTO>()
                .ForMember(grfsdto => grfsdto.Name, x => x.MapFrom(urm => urm.Role.Name));

            this.CreateMap<User, GetUserIdDTO>();
        }
    }
}
