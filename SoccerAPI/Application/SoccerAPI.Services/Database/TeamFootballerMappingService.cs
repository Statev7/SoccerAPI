namespace SoccerAPI.Services.Database
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.Services.Database.Contracts;

    public class TeamFootballerMappingService : BaseService<TeamFootballerMapping>, ITeamFootballerMappingService
    {
        public TeamFootballerMappingService(SoccerAPIDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {

        }

        public async Task<T> GetByTeamAndFootballerIdAsync<T>(Guid teamId, Guid footbollerId)
        {
            var teamFootboolerReletion = await this.DbSet
                .Where(tfm => tfm.TeamId == teamId && tfm.FootballerId == footbollerId)
                .Include(tfm => tfm.Team)
                .Include(tfm => tfm.Footballer)
                .SingleOrDefaultAsync();

            if (teamFootboolerReletion == null)
            {
                //TODO catch exception!
                throw new ArgumentException(ExceptionMessages.TEAM_FOOTBOOLER_MAPPING_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            var mapped = this.Mapper.Map<T>(teamFootboolerReletion);
            return mapped;
        }

        public async Task<T> AddAsync<T>(TeamFootballerMapping teamFootballerMapping)
        {
            TeamFootballerMapping mappingModel = this.Mapper.Map<TeamFootballerMapping>(teamFootballerMapping);

            await this.DbSet.AddAsync(mappingModel);
            await this.DbContext.SaveChangesAsync();

            T result = this.Mapper.Map<T>(mappingModel);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid teamId, Guid footballerId)
        {
            var teamFootboolerReletionToDelete = 
                await this.GetByTeamAndFootballerIdAsync<TeamFootballerMapping>(teamId, footballerId);

            this.DbSet.Remove(teamFootboolerReletionToDelete);
            await this.DbContext.SaveChangesAsync();

            return true;
        }
    }
}
