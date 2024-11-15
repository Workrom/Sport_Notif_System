using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Interfaces.InMemory
{
    public class InMemoryTrainerRepository : ITrainerRepository
    {
        private readonly List<Trainer> _trainers = new List<Trainer>();
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
        public void UpdateTrainer(Trainer updatedTrainer)
        {
            var trainer = _trainers.FirstOrDefault(c => c.Name == updatedTrainer.Name);

            if (trainer != null)
            {
                trainer.Name = updatedTrainer.Name;
                trainer.Age = updatedTrainer.Age;
            }
            else
            {
                throw new Exception("Trainer not found");
            }
        }
    }
}
