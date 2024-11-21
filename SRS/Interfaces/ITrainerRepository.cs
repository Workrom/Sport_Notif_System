using SRS.Models;

namespace SRS.Interfaces
{
    public interface ITrainerRepository
    {
        Trainer GetTrainerByName(string name);
        void AddTrainer(Trainer trainer);
        void RemoveTrainer(Trainer trainer);
        void UpdateTrainer(Trainer currentTrainer, Trainer updatedTrainer);
    }
}
