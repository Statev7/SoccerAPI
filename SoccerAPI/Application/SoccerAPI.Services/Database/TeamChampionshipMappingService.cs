namespace SoccerAPI.Services.Database
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.Services.Database.Contracts;

    public class TeamChampionshipMappingService : BaseService<TeamChampionshipMapping>, ITeamChampionshipMappingService
    {
        public TeamChampionshipMappingService(SoccerAPIDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {

        }

        public async Task<T> GetByChampionshipAndTeamIdAsync<T>(Guid championshipId, Guid teamId)
        {
            var championshipTeamReletion = await this.DbSet
                .Where(tcm => tcm.ChampionshipId == championshipId && tcm.TeamId == teamId)
                .Include(tcm => tcm.Championship)
                .Include(tcm => tcm.Team)
                .SingleOrDefaultAsync();

            if (championshipTeamReletion == null)
            {
                //TODO throw exception
            }

            T mapped = this.Mapper.Map<T>(championshipTeamReletion);
            return mapped;
        }

        public async Task<T> AddAsync<T>(TeamChampionshipMapping teamChampionshipMapping)
        {
            TeamChampionshipMapping mappingModel = this.Mapper.Map<TeamChampionshipMapping>(teamChampionshipMapping);

            await this.DbSet.AddAsync(mappingModel);
            await this.DbContext.SaveChangesAsync();

            T result = this.Mapper.Map<T>(mappingModel);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid championshipId, Guid teamId)
        {
            var championshipTeamReletionToDelete = 
               await this.GetByChampionshipAndTeamIdAsync<TeamChampionshipMapping>(championshipId, teamId);

            this.DbSet.Remove(championshipTeamReletionToDelete);
            await this.DbContext.SaveChangesAsync();

            return true;
        }
    }
}
