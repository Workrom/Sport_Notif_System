using SRS.Models;

namespace SRS.Interfaces.InMemory
{
    public class InMemoryTrainerRepository : ITrainerRepository
    {
        private readonly List<Trainer> _trainers = new List<Trainer>();
        public IEnumerable<Trainer> GetAllTrainers()
        {
            return _trainers.AsReadOnly();
        }
        public Trainer GetTrainerByName(string name)
        {
            return _trainers.FirstOrDefault(t => t.Name == name);
        }
        public void AddTrainer (Trainer trainer)
        {
            _trainers.Add(trainer);
        }
        public void RemoveTrainer (Trainer trainer)
        {
            _trainers.Remove(trainer);
        }
        public void UpdateTrainer(Trainer currentTrainer, Trainer updatedTrainer)
        {
            currentTrainer = _trainers.FirstOrDefault(c => c.Name == currentTrainer.Name);

            if (currentTrainer != null)
            {
                currentTrainer.Name = updatedTrainer.Name;
                currentTrainer.Age = updatedTrainer.Age;
            }
            else
            {
                throw new Exception("Trainer not found");
            }
        }
        public int GetCount()
        {
            return _trainers.Count;
        }
    }
}
