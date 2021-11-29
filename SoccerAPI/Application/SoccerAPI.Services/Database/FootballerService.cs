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
    using SoccerAPI.Services.Database.Contracts;

    public class FootballerService : BaseService<Footballer>, IFootballerService
    {
        public FootballerService(SoccerAPIDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public async Task<T> GetAllAsync<T>()
        {
            List<Footballer> allFootballers = await this.DbSet.ToListAsync();

            T mappedFootballers = this.Mapper.Map<T>(allFootballers);

            return mappedFootballers;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Footballer footballer = await this.DbSet
                .SingleOrDefaultAsync(f => f.Id == id);

            T mappedFootballer = this.Mapper.Map<T>(footballer);

            return mappedFootballer;
        }

        public async Task<Footballer> AddAsync(PostFootballerDTO footballer)
        {
            Footballer footballerToCreate = this.Mapper.Map<Footballer>(footballer);

            await this.DbSet.AddAsync(footballerToCreate);
            await this.DbContext.SaveChangesAsync();

            return footballerToCreate;
        }

        public async Task<bool> UpdateAsync(Guid id, PutFootballerDTO footballer)
        {
            Footballer footballerToUpdate = await this.GetByIdAsync<Footballer>(id);

            if (footballerToUpdate == null)
            {
                return false;
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
                return false;
            }

            PropertyInfo[] properties = footballer.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(footballer);

                bool isValid = this.IsNullOrDefault<object>(propertyValue);
                if (isValid == false)
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
                return false;
            }

            this.DbSet.Remove(footballerToDelete);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        private bool IsNullOrDefault<T>(T argument)
        {
            // deal with normal scenarios
            if (argument == null)
            {
                return true;
            }
            if (object.Equals(argument, default(T)))
            {
                return true;
            }

            // deal with non-null nullables
            Type methodType = typeof(T);
            if (Nullable.GetUnderlyingType(methodType) != null)
            {
                return false;
            }

            // deal with boxed value types
            Type argumentType = argument.GetType();
            if (argumentType.IsValueType && argumentType != methodType)
            {
                object obj = Activator.CreateInstance(argument.GetType());
                return obj.Equals(argument);
            }

            return false;
        }
    }
}
