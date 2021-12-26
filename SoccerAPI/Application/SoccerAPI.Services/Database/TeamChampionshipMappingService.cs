namespace SoccerAPI.Services.Database
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Common.Exeptions;
    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Championships;
    using SoccerAPI.Services.Database.Contracts;

    public class TeamChampionshipMappingService : BaseService<ChampionshipTeamMapping>, ITeamChampionshipMappingService
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
                throw new EntityDoesNotExistException(ExceptionMessages.CHAMPIONSHIP_TEAM_MAPPING_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            T mapped = this.Mapper.Map<T>(championshipTeamReletion);
            return mapped;
        }

        public async Task<T> AddAsync<T>(ChampionshipTeamMapping teamChampionshipMapping)
        {
            ChampionshipTeamMapping mappingModel = this.Mapper.Map<ChampionshipTeamMapping>(teamChampionshipMapping);

            await this.DbSet.AddAsync(mappingModel);
            await this.DbContext.SaveChangesAsync();

            T result = this.Mapper.Map<T>(mappingModel);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid championshipId, Guid teamId)
        {
            var championshipTeamReletionToDelete = 
               await this.GetByChampionshipAndTeamIdAsync<ChampionshipTeamMapping>(championshipId, teamId);

            this.DbSet.Remove(championshipTeamReletionToDelete);
            await this.DbContext.SaveChangesAsync();

            return true;
        }
    }
}
