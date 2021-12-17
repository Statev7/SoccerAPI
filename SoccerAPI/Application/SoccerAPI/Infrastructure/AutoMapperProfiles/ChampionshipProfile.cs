namespace SoccerAPI.Infrastructure.AutoMapperProfiles
{
    using System.Collections.Generic;

    using AutoMapper;

    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Championship;

    public class ChampionshipProfile : Profile
    {
        public ChampionshipProfile()
        {
            this.CreateMap<PostChampionshipDTO, Championship>();
            this.CreateMap<PutChampionshipDTO, Championship>();
            this.CreateMap<Championship, GetChampionshipDTO>()
                .ForMember(gcdto => gcdto.TeamsCount, c => c.MapFrom(c => c.Teams.Count));

            this.CreateMap<IEnumerable<Championship>, GetAllChampionshipsDTO>()
                .ForMember(gac => gac.Championships, t => t.MapFrom(championships => championships));
        }
    }
}
