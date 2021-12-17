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

            this.CreateMap<IEnumerable<TeamFootballerMapping>, GetAllFootballersDTO>()
                .ForMember(gafdto => gafdto.Footballers, f => f.MapFrom(footballers => footballers));

            this.CreateMap<TeamFootballerMapping, GetFootballerDTO>()
                .ForMember(gfdto => gfdto.Id, tfm => tfm.MapFrom(mapping => mapping.FootballerId))
                .ForMember(gfdto => gfdto.FirstName, tfm => tfm.MapFrom(mappin => mappin.Footballer.FirstName))
                .ForMember(gfdto => gfdto.SecondName, tfm => tfm.MapFrom(mappin => mappin.Footballer.SecondName))
                .ForMember(gfdto => gfdto.Nationality, tfm => tfm.MapFrom(mappin => mappin.Footballer.Nationality))
                .ForMember(gfdto => gfdto.DateOfBirth, tfm => tfm.MapFrom(mappin => mappin.Footballer.DateOfBirth))
                .ForMember(gfdto => gfdto.Age, tfm => tfm.MapFrom(mappin => mappin.Footballer.Age))
                .ForMember(gfdto => gfdto.Position, tfm => tfm.MapFrom(mappin => mappin.Footballer.Position))
                .ForMember(gfdto => gfdto.Salary, tfm => tfm.MapFrom(mappin => mappin.Footballer.Salary))
                .ForMember(gfdto => gfdto.StrongLeg, tfm => tfm.MapFrom(mappin => mappin.Footballer.StrongLeg))
                .ForMember(gfdto => gfdto.TeamsCount, tfm => tfm.MapFrom(mappin => mappin.Footballer.Teams.Count));

            this.CreateMap<PostFootballerDTO, Footballer>();
            this.CreateMap<PutFootballerDTO, Footballer>();
        }
    }
}
