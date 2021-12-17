namespace SoccerAPI.Services.Database
{
    using System.Threading.Tasks;

    using AutoMapper;

    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.Services.Database.Contracts;

    public class TeamChampionshipMappingService : BaseService<TeamChampionshipMapping>, ITeamChampionshipMappingService
    {
        public TeamChampionshipMappingService(SoccerAPIDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {

        }

        public async Task<T> AddAsync<T>(TeamChampionshipMapping teamChampionshipMapping)
        {
            TeamChampionshipMapping mappingModel = this.Mapper.Map<TeamChampionshipMapping>(teamChampionshipMapping);

            await this.DbSet.AddAsync(mappingModel);
            await this.DbContext.SaveChangesAsync();

            T result = this.Mapper.Map<T>(mappingModel);
            return result;
        }
    }
}
