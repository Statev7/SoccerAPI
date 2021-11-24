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
            this.CreateMap<Team, GetTeamDTO>();

            this.CreateMap<IEnumerable<Team>, GetAllTeamsDTO>()
                .ForMember(gat => gat.Teams, t => t.MapFrom(teams => teams));
        }
    }
}
