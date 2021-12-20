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

            this.CreateMap<IEnumerable<TeamChampionshipMapping>, GetAllChampionshipsDTO>()
                .ForMember(gafdto => gafdto.Championships, c => c.MapFrom(championship => championship));

            this.CreateMap<TeamChampionshipMapping, GetChampionshipDTO>()
                .ForMember(gcdto => gcdto.Id, c => c.MapFrom(tcm => tcm.Championship.Id))
                .ForMember(gcdto => gcdto.Name, c => c.MapFrom(tcm => tcm.Championship.Name))
                .ForMember(gcdto => gcdto.TeamsCount, c => c.MapFrom(tcm => tcm.Championship.Teams.Count));
            
        }
    }
}
