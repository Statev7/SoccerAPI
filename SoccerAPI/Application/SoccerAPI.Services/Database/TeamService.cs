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
    using SoccerAPI.DTOs.Team;
    using SoccerAPI.Services.Database.Contracts;
    using SoccerAPI.Services.Validator;

    public class TeamService : BaseService<Team>, ITeamService
    {
        private readonly IFootballerService footballerService;
        private readonly ITeamFootballerMappingService teamFootballerMappingService;

        public TeamService(SoccerAPIDbContext dbContext, 
            IMapper mapper, 
            IFootballerService footballerService,
            ITeamFootballerMappingService teamFootballerMappingService)
            : base(dbContext, mapper)
        {
            this.footballerService = footballerService;
            this.teamFootballerMappingService = teamFootballerMappingService;
        }

        public async Task<T> GetAllAsync<T>()
        {
            List<Team> teams = await this.DbSet
                .Include(t => t.Championships)
                .Include(t => t.Footballers)
                .Include(t => t.Coaches)
                .OrderBy(t => t.Name)
                .ToListAsync();

            T mappedTeams = this.Mapper.Map<T>(teams);

            return mappedTeams;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Team team = await this.DbSet
                .Include(t => t.Championships)
                .Include(t => t.Footballers)
                .Include(t => t.Coaches)
                .SingleOrDefaultAsync(t => t.Id == id);

            T mappedTeam = this.Mapper.Map<T>(team);

            return mappedTeam;
        }

        public async Task<Team> AddAsync(PostTeamDTO team)
        {
            Team teamToAdd = this.Mapper.Map<Team>(team);

            await this.DbSet.AddAsync(teamToAdd);
            await this.DbContext.SaveChangesAsync();

            return teamToAdd;
        }

        public async Task<bool> UpdateAsync(Guid id, PutTeamDTO team)
        {
            Team teamToUpdate = await this.GetByIdAsync<Team>(id);

            if (teamToUpdate == null)
            {
                return false;
            }

            Team updatedTeam = this.Mapper.Map(team, teamToUpdate);

            updatedTeam.UpdatedOn = DateTime.UtcNow;

            this.DbContext.Update(updatedTeam);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PartialUpdateAsync(Guid id, PatchTeamDTO team)
        {
            Team teamToUpdate = await this.GetByIdAsync<Team>(id);

            if (teamToUpdate == null)
            {
                return false;
            }

            PropertyInfo[] properties = team.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(team);

                bool isNullOrDefault = Validator.IsNullOrDefault<object>(propertyValue);
                if (isNullOrDefault == false)
                {
                    Type propertyType = property.PropertyType;
                    bool isIEnumerable = propertyType.IsGenericType
                        && propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                    if (isIEnumerable)
                    {
                        switch (property.Name)
                        {
                            case "FootballersId":
                                await this.AddFootballersToTeamAsync(team, teamToUpdate, property);
                                break;
                        }

                        continue;
                    }

                    PropertyInfo propertyToUpdate = teamToUpdate.GetType().GetProperty(property.Name);
                    propertyToUpdate.SetValue(teamToUpdate, propertyValue);
                }
            }

            teamToUpdate.UpdatedOn = DateTime.UtcNow;

            this.DbContext.Update(teamToUpdate);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Team teamToDelete = await this.GetByIdAsync<Team>(id);

            if (teamToDelete == null)
            {
                return false;
            }

            this.DbSet.Remove(teamToDelete);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        private async Task AddFootballersToTeamAsync(PatchTeamDTO team, Team teamToUpdate, PropertyInfo property)
        {
            IEnumerable<Guid> ids = property.GetValue(team) as IEnumerable<Guid>;
            foreach (var id in ids)
            {
                Footballer footballerToAdd = await this.footballerService.GetByIdAsync<Footballer>(id);
                if (footballerToAdd == null)
                {
                    continue;
                }

                bool isAlreadyExist = teamToUpdate.Footballers.Any(tfm => tfm.TeamId == teamToUpdate.Id && tfm.FootballerId == id);
                if (isAlreadyExist)
                {
                    continue;
                }

                TeamFootballerMapping teamFootballerMapping = new TeamFootballerMapping();
                teamFootballerMapping.FootballerId = id;
                teamFootballerMapping.TeamId = teamToUpdate.Id;

                await this.teamFootballerMappingService.AddAsync<TeamFootballerMapping>(teamFootballerMapping);
            }
        }
    }
}
