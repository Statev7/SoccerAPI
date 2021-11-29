namespace SoccerAPI.Infrastructure.AutoMapperProfiles
{
    using System.Collections.Generic;

    using AutoMapper;

    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Footballer;

    public class FootballerProfile : Profile
    {
        public FootballerProfile()
        {
            this.CreateMap<Footballer, GetFootballerDTO>();

            this.CreateMap<IEnumerable<Footballer>, GetAllFootballersDTO>()
                .ForMember(gaf => gaf.Footballers, f => f.MapFrom(footballer => footballer));

            this.CreateMap<PostFootballerDTO, Footballer>();
            this.CreateMap<PutFootballerDTO, Footballer>();
        }
    }
}
