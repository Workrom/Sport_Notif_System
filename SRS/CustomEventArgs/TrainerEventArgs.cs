using SRS.Models;

namespace SRS.CustomEventArgs
{
    public class TrainerEventArgs(Trainer? previousTrainer, Trainer trainer) : EventArgs
    {
        public Trainer? previousTrainer { get; } = previousTrainer;
        public Trainer Trainer { get; } = trainer;
    }
}
