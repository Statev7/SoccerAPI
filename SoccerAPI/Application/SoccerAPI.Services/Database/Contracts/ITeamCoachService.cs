namespace SoccerAPI.Services.Database.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface ITeamCoachService 
    {
        Task<bool> DeleteAsync(Guid teamId, Guid coachId);
    }
}
