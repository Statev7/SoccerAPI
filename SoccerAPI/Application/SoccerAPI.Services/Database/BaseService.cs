namespace SoccerAPI.Services.Database
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    using SoccerAPI.Database;
    using SoccerAPI.Database.Models.BaseModels;

    public abstract class BaseService<T>
        where T : BaseModel
    {
        private readonly IActionContextAccessor actionContextAccessor;

        public BaseService(SoccerAPIDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.DbSet = dbContext.Set<T>();
            this.Mapper = mapper;
        }

        protected BaseService(SoccerAPIDbContext dbContext, IMapper mapper, IActionContextAccessor actionContextAccessor)
            :this(dbContext, mapper)
        {
            this.actionContextAccessor = actionContextAccessor;
        }

        protected IMapper Mapper { get; }

        protected SoccerAPIDbContext DbContext { get; private set; }

        protected DbSet<T> DbSet { get; private set; }

        protected void AddModelError(string key, string message)
        {
            this.actionContextAccessor.ActionContext.ModelState.AddModelError(key, message);
        }
    }
}
