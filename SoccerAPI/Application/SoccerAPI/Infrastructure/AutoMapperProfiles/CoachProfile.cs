namespace SoccerAPI.Infrastructure.AutoMapperProfiles
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Coach;

    public class CoachProfile : Profile
    {
        public CoachProfile()
        {
            this.CreateMap<PostCoachDTO, Coach>();
            this.CreateMap<PutCoachDTO, Coach>();

            this.CreateMap<Coach, GetCoachDTO>();

            this.CreateMap<IEnumerable<Coach>, GetAllCoachesDTO>()
                .ForMember(gacdto => gacdto.Coaches, c => c.MapFrom(coach => coach));
        }
    }
}
