namespace SoccerAPI.Services.Database
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.Services.Database.Contracts;

    public class TeamFootballerMappingService : BaseService<TeamFootballerMapping>, ITeamFootballerMappingService
    {
        public TeamFootballerMappingService(SoccerAPIDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public Task<T> GetAllAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> AddAsync<T>(TeamFootballerMapping teamFootballerMapping)
        {
            TeamFootballerMapping mappingModel = this.Mapper.Map<TeamFootballerMapping>(teamFootballerMapping);

            await this.DbSet.AddAsync(mappingModel);
            await this.DbContext.SaveChangesAsync();

            T result = this.Mapper.Map<T>(mappingModel);
            return result;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
