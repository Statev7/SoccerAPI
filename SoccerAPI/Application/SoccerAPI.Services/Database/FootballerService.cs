namespace SoccerAPI.Services.Database
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.Teams;
    using SoccerAPI.DTOs.Footballer;
    using SoccerAPI.Services.Validator;
    using SoccerAPI.Services.Database.Contracts;
    using SoccerAPI.Common.Exeptions;
    using SoccerAPI.Common.Constants;
    using SoccerAPI.Common.Constants.ModelConstants;

    public class FootballerService : BaseService<Footballer>, IFootballerService
    {
        public FootballerService(SoccerAPIDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public async Task<T> GetAllAsync<T>()
        {
            List<Footballer> allFootballers = await this.DbSet
                .Include(f => f.Teams)
                .ToListAsync();

            T mappedFootballers = this.Mapper.Map<T>(allFootballers);

            return mappedFootballers;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Footballer footballer = await this.DbSet
                .Include(f => f.Teams)
                .SingleOrDefaultAsync(f => f.Id == id);

            T mappedFootballer = this.Mapper.Map<T>(footballer);

            return mappedFootballer;
        }

        public async Task<Footballer> AddAsync(PostFootballerDTO footballer)
        {
            Footballer footballerToCreate = this.Mapper.Map<Footballer>(footballer);

            bool isValid = Validator.IsDateValid(footballer.DateOfBirth);
            if (isValid == false)
            {
                throw new InvalidPropertyDateException(ExceptionMessages.INVALID_DATE_OF_BIRTH_ERROR_MESSAGE);
            }

            if (footballerToCreate.Age < FootballerConstants.MIN_AGE)
            {
                string message = string.Format(ExceptionMessages.COACH_INVALID_AGE_ERROR_MESSAGE, FootballerConstants.MIN_AGE);

                throw new InvalidPropertyDateException(message);
            }

            await this.DbSet.AddAsync(footballerToCreate);
            await this.DbContext.SaveChangesAsync();

            return footballerToCreate;
        }

        public async Task<bool> UpdateAsync(Guid id, PutFootballerDTO footballer)
        {
            Footballer footballerToUpdate = await this.GetByIdAsync<Footballer>(id);

            if (footballerToUpdate == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.FOOTBALLER_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            Footballer updatedFootballer = this.Mapper.Map(footballer, footballerToUpdate);
            updatedFootballer.UpdatedOn = DateTime.UtcNow;

            this.DbContext.Update(updatedFootballer);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PartialUpdateAsync(Guid id, PatchFootballerDTO footballer)
        {
            Footballer footballerToUpdate = await this.GetByIdAsync<Footballer>(id);

            if (footballerToUpdate == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.FOOTBALLER_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            PropertyInfo[] properties = footballer.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(footballer);

                bool isNullOrDefault = Validator.IsNullOrDefault<object>(propertyValue);
                if (isNullOrDefault == false)
                {
                    PropertyInfo propertyToUpdate = footballerToUpdate.GetType().GetProperty(property.Name);
                    propertyToUpdate.SetValue(footballerToUpdate, propertyValue);
                }
            }

            footballerToUpdate.UpdatedOn = DateTime.UtcNow;

            this.DbContext.Update(footballerToUpdate);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Footballer footballerToDelete = await this.GetByIdAsync<Footballer>(id);

            if (footballerToDelete == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.FOOTBALLER_DOES_NOT_EXIST_ERROR_MESSAGE);
            }

            this.DbSet.Remove(footballerToDelete);
            await this.DbContext.SaveChangesAsync();

            return true;
        }
    }
}
