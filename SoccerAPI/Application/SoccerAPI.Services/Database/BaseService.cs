namespace SoccerAPI.Services.Database
{
    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.BaseModels;

    public abstract class BaseService<T>
        where T : BaseModel
    {
        protected BaseService(SoccerAPIDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.DbSet = dbContext.Set<T>();
            this.Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        protected SoccerAPIDbContext DbContext { get; private set; }

        protected DbSet<T> DbSet { get; private set; }
    }
}
