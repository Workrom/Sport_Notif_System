
namespace SRS.Models
{
    public class Trainer
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
        public List<TrainingSession> Sessions { get; set; } = new List<TrainingSession>();
    }
}
