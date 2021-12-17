﻿namespace SoccerAPI.Services.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Common.Constants.ModelConstants;
    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Team;
    using SoccerAPI.Services.Database.Contracts;
    using SoccerAPI.Services.Validator;

    public class TeamService : BaseService<Team>, ITeamService
    {
        private readonly ITeamFootballerMappingService teamFootballerMappingService;
        private readonly IFootballerService footballerService;

        public TeamService(SoccerAPIDbContext dbContext,
            IMapper mapper,
            IActionContextAccessor actionContextAccessor,
            ITeamFootballerMappingService teamFootballerMappingService,
            IFootballerService footballerService)
            : base(dbContext, mapper, actionContextAccessor)
        {
            this.teamFootballerMappingService = teamFootballerMappingService;
            this.footballerService = footballerService;
        }

        public async Task<T> GetAllAsync<T>()
        {
            List<Team> teams = await this.DbSet
                .Include(t => t.Footballers)
                .ThenInclude(f => f.Footballer)
                .Include(t => t.Championships)
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
                    this.AddModelError($"{id}", ExceptionMessages.FOOTBALLER_NOT_EXIST_ERROR_MESSAGE);
                    continue;
                }

                bool isFootballerAlreadyExist = teamToUpdate.Footballers
                    .Any(tfm => tfm.TeamId == teamToUpdate.Id && tfm.FootballerId == id);

                if (isFootballerAlreadyExist)
                {
                    this.AddModelError($"{footballerToAdd.Id}", ExceptionMessages.FOOTBALLER_IS_ALREADY_IN_THE_TEAM_ERROR_MESSAGE);
                    continue;
                }

                if (footballerToAdd.Teams.Count >= FootballerConstants.MAX_NUMBER_OF_TEAMS)
                {
                    this.AddModelError($"{footballerToAdd.Id}", ExceptionMessages.A_FOOTBALLER_CANNOT_HAVE_MORE_TEAMS_ERROR_MESSAGE);
                    continue;
                }

                bool isThereAFreePlaceInTheTeam = teamToUpdate.Footballers.Count < TeamConstants.MAX_PLAYERS_PER_TEAM;
                if (isThereAFreePlaceInTheTeam == false)
                {
                    string message = string.Format(ExceptionMessages.TEAM_PLAYERS_COUNT_ERROR_MESSAGE, TeamConstants.MAX_PLAYERS_PER_TEAM);
                    this.AddModelError($"{teamToUpdate.Id}", message);
                    break;
                }

                TeamFootballerMapping teamFootballerMapping = new TeamFootballerMapping();
                teamFootballerMapping.FootballerId = id;
                teamFootballerMapping.TeamId = teamToUpdate.Id;

                await this.teamFootballerMappingService.AddAsync<TeamFootballerMapping>(teamFootballerMapping);
            }
        }
    }
}
