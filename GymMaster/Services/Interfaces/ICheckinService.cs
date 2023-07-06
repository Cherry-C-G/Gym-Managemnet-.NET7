using Entities;

namespace Services.Interfaces
{
    public interface ICheckinService
    {
        Task<bool> AlreadyCheckedIn(int memberId);
        Task<bool?> DeleteAsync(int id);
        Task<Checkin?> GetById(int id);
        Task<List<Checkin>> GetCheckins();
        Task<Checkin> SaveAsync(Checkin checkin);
        Task<bool?> UpdateAsync(int id, Checkin checkin);
    }
}