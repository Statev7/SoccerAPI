namespace SoccerAPI.Services.Database
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Common.Exeptions;
    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.Services.Database.Contracts;

    public class TeamCoachService : BaseService<Team>, ITeamCoachService
    {
        private readonly ITeamService teamService;
        private readonly ICoachService coachService;

        public TeamCoachService(
            SoccerAPIDbContext dbContext, 
            IMapper mapper,
            ITeamService teamService,
            ICoachService coachService) 
            : base(dbContext, mapper)
        {
            this.teamService = teamService;
            this.coachService = coachService;
        }

        public async Task<bool> DeleteAsync(Guid teamId, Guid coachId)
        {
            bool result = await this.CheckIfTeamCoachHaveReletion(teamId, coachId);

            if (result == false)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.TEAM_COACH_MAPPING_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            Team team = await this.teamService.GetByIdAsync<Team>(teamId);
            Coach coach = await this.coachService.GetByIdAsync<Coach>(coachId);

            team.Coaches.Remove(coach);
            coach.Team = null;
            coach.TeamId = null;

            this.DbSet.Update(team);
            await this.DbContext.SaveChangesAsync();

            return result;
        }

        private async Task<bool> CheckIfTeamCoachHaveReletion(Guid teamId, Guid coachId)
        {
            Team team = await this.teamService.GetByIdAsync<Team>(teamId);
            Coach coach = await this.coachService.GetByIdAsync<Coach>(coachId);

            if (team == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.TEAM_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            if (coach == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.COACH_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            bool isValid = team.Coaches.Any(c => c.Id == coachId) && coach.TeamId == teamId;

            return isValid;
        }
    }
}
