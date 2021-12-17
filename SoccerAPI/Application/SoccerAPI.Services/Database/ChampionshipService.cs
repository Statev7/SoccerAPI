namespace SoccerAPI.Services.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Championship;
    using SoccerAPI.Services.Database.Contracts;
    using SoccerAPI.Services.Validator;

    public class ChampionshipService : BaseService<Championship>, IChampionshipService
    {
        private readonly ITeamService teamService;
        private readonly ITeamChampionshipMappingService teamChampionshipMapping;

        public ChampionshipService(SoccerAPIDbContext dbContext, 
            IMapper mapper, 
            ITeamService teamService,
            ITeamChampionshipMappingService teamChampionshipMapping)
            : base(dbContext, mapper)
        {
            this.teamService = teamService;
            this.teamChampionshipMapping = teamChampionshipMapping;
        }

        public async Task<T> GetAllAsync<T>()
        {
            List<Championship> championships = await this.DbSet
                 .Include(c => c.Teams)
                 .ToListAsync();

            T mappedChampionships = this.Mapper.Map<T>(championships);

            return mappedChampionships;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Championship championship = await this.DbSet
                .Include(f => f.Teams)
                .SingleOrDefaultAsync(f => f.Id == id);

            T mappedChampionship = this.Mapper.Map<T>(championship);

            return mappedChampionship;
        }

        public async Task<Championship> AddAsync(PostChampionshipDTO championship)
        {
            Championship championshipToCreate = this.Mapper.Map<Championship>(championship);

            await this.DbSet.AddAsync(championshipToCreate);
            await this.DbContext.SaveChangesAsync();

            return championshipToCreate;
        }

        public async Task<bool> UpdateAsync(Guid id, PutChampionshipDTO championship)
        {
            Championship championshipToUpdate = await this.GetByIdAsync<Championship>(id);

            if (championshipToUpdate == null)
            {
                return false;
            }

            Championship mappedChampinship = this.Mapper.Map(championship, championshipToUpdate);
            mappedChampinship.UpdatedOn = DateTime.UtcNow;

            this.DbContext.Update(mappedChampinship);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PartialUpdateAsync(Guid id, PatchChampionshipDTO championship)
        {
            Championship championshipToUpdate = await this.GetByIdAsync<Championship>(id);

            if (championshipToUpdate == null)
            {
                return false;
            }

            PropertyInfo[] properties = championship.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(championship);

                bool isNullOrDefault = Validator.IsNullOrDefault<object>(propertyValue);
                if (isNullOrDefault == false)
                {
                    Type propertyType = property.PropertyType;
                    bool isIEnumerable = propertyType.IsGenericType
                        && propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                    if (isIEnumerable)
                    {
                        await this.AddTeamToChampionshipAsync(championship, championshipToUpdate, property);
                        continue;
                    }

                    PropertyInfo propertyToUpdate = championshipToUpdate.GetType().GetProperty(property.Name);
                    propertyToUpdate.SetValue(championshipToUpdate, propertyValue);
                }
            }

            championshipToUpdate.UpdatedOn = DateTime.UtcNow;

            this.DbContext.Update(championshipToUpdate);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Championship championshipToDelete = await this.GetByIdAsync<Championship>(id);

            if (championshipToDelete == null)
            {
                return false;
            }

            this.DbSet.Remove(championshipToDelete);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        private async Task AddTeamToChampionshipAsync(PatchChampionshipDTO championship, Championship championshipToUpdate, PropertyInfo property)
        {
            IEnumerable<Guid> ids = property.GetValue(championship) as IEnumerable<Guid>;
            foreach (var id in ids)
            {
                Team teamToAdd = await this.teamService.GetByIdAsync<Team>(id);

                if (teamToAdd == null)
                {
                    //TODO throw exception!
                }

                bool isTeamAlreadyExist = championshipToUpdate.Teams
                    .Any(tcm => tcm.ChampionshipId == championshipToUpdate.Id && tcm.TeamId == id);

                if (isTeamAlreadyExist)
                {
                    //TODO throw exception!
                }

                //TODO Add constants
                bool areThereAnyVacancies = championshipToUpdate.Teams.Count < 20;
                if (areThereAnyVacancies == false)
                {
                    //TODO throw exception!
                }

                TeamChampionshipMapping teamChampionshipMapping = new TeamChampionshipMapping();
                teamChampionshipMapping.TeamId = id;
                teamChampionshipMapping.ChampionshipId = championshipToUpdate.Id;

                await this.teamChampionshipMapping.AddAsync<TeamChampionshipMapping>(teamChampionshipMapping);
            }
        }
    }
}
