namespace SoccerAPI.Infrastructure.AutoMapperProfiles
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Footballer;

    public class FootballerProfile : Profile
    {
        public FootballerProfile()
        {
            this.CreateMap<Footballer, GetFootballerDTO>()
                .ForMember(gfdto => gfdto.TeamsCount, f => f.MapFrom(f => f.Teams.Count));

            this.CreateMap<IEnumerable<Footballer>, GetAllFootballersDTO>()
                .ForMember(gafdto => gafdto.Footballers, f => f.MapFrom(footballer => footballer));

            this.CreateMap<PostFootballerDTO, Footballer>();
            this.CreateMap<PutFootballerDTO, Footballer>();
        }
    }
}
