namespace SoccerAPI.Services.Database
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Common.Constants;
    using SoccerAPI.Validator;
    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Coach;
    using SoccerAPI.Services.Database.Contracts;
    using SoccerAPI.Common.Exeptions;
    using SoccerAPI.Common.Constants.ModelConstants;

    public class CoachService : BaseService<Coach>, ICoachService
    {
        public CoachService(
            SoccerAPIDbContext dbContext,
            IMapper mapper,
            IActionContextAccessor actionContextAccessor)
            : base(dbContext, mapper, actionContextAccessor)
        {

        }

        public async Task<T> GetAllAsync<T>()
        {
            List<Coach> coaches = await this.DbSet
                .Include(c => c.Team)
                .ToListAsync();

            T mapped = this.Mapper.Map<T>(coaches);

            return mapped;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Coach coach = await this.DbSet
                .Include(c => c.Team)
                .SingleOrDefaultAsync(c => c.Id == id);

            T mappedCoach = this.Mapper.Map<T>(coach);

            return mappedCoach;
        }

        public async Task<Coach> AddAsync(PostCoachDTO model)
        {
            Coach coach = this.Mapper.Map<Coach>(model);

            bool isValid = Validator.IsDateValid(model.DateOfBirth);
            if (isValid == false)
            {
                throw new InvalidPropertyDateException(ExceptionMessages.INVALID_DATE_OF_BIRTH_ERROR_MESSAGE);
            }

            if (coach.Age < CoachConstants.MIN_AGE)
            {
                string message = string.Format(ExceptionMessages.COACH_INVALID_AGE_ERROR_MESSAGE, CoachConstants.MIN_AGE);

                throw new InvalidPropertyDateException(message);
            }

            await this.DbSet.AddAsync(coach);
            await this.DbContext.SaveChangesAsync();

            return coach;
        }

        public async Task<bool> UpdateAsync(Guid id, PutCoachDTO model)
        {
            Coach coachToUpdate = await this.GetByIdAsync<Coach>(id);

            if (coachToUpdate == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.COACH_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            Coach mappedCoach = this.Mapper.Map(model, coachToUpdate);
            mappedCoach.UpdatedOn = DateTime.UtcNow;

            this.DbSet.Update(mappedCoach);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PartialUpdateAsync(Guid id, PatchCoachDTO model)
        {
            Coach coachToUpdate = await this.GetByIdAsync<Coach>(id);

            if (coachToUpdate == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.COACH_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            PropertyInfo[] properties = model.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(model);

                bool isNullOrDefault = Validator.IsNullOrDefault<object>(propertyValue);
                if (isNullOrDefault == false)
                {
                    PropertyInfo propertyToUpdate = coachToUpdate.GetType().GetProperty(property.Name);
                    propertyToUpdate.SetValue(coachToUpdate, propertyValue);
                }
            }

            coachToUpdate.UpdatedOn = DateTime.UtcNow;

            this.DbContext.Update(coachToUpdate);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Coach coachToDelete = await this.GetByIdAsync<Coach>(id);

            if (coachToDelete == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.COACH_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            this.DbSet.Remove(coachToDelete);
            await this.DbContext.SaveChangesAsync();

            return true;
        }
    }
}
