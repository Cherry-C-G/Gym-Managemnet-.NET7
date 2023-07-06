using Entities;

namespace Services.Interfaces
{
    public interface ITrainerService
    {
        Task<bool?> DeleteAsync(int id);
        Task<Trainer?> GetById(int id);
        Task<List<Trainer>> GetTrainers();
        Task<int> GetTrainersCount();
        Task<Trainer> SaveAsync(Trainer trainer);
        Task<bool?> UpdateAsync(int id, Trainer trainer);
    }
}