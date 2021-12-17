namespace SoccerAPI.Infrastructure.AutoMapperProfiles
{
    using System.Collections.Generic;

    using AutoMapper;

    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Team;

    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            this.CreateMap<PostTeamDTO, Team>();
            this.CreateMap<Team, GetTeamDTO>()
                .ForMember(gtd => gtd.Footballers, f => f.MapFrom(footballer => footballer.Footballers));

            this.CreateMap<IEnumerable<Team>, GetAllTeamsDTO>()
                .ForMember(gat => gat.Teams, t => t.MapFrom(teams => teams));

            this.CreateMap<PutTeamDTO, Team>();
        }
    }
}
