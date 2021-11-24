namespace SoccerAPI.Services.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Team;
    using SoccerAPI.Services.Database.Contracts;

    public class TeamService : BaseService<Team>, ITeamService
    {
        public TeamService(SoccerAPIDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
            
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
    }
}
