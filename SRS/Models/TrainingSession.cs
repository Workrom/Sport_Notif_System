
namespace SRS.Models
{
    public class TrainingSession
    {
        public required string Type { get; set; }
        public required int Capacity { get; set; }
        public enum StatusType
        {
            FREE,
            FULL,
            BUSY,
            DONE
        }
        private StatusType _status;
        public StatusType Status => _status;
        public List<Client> Clients { get; set; } = new List<Client>();
        public Client? VIPClient { get; set; }

        public TrainingSession()
        {
            _status = StatusType.FREE;
        }
        public void UpdateStatus(StatusType newStatus)
        {
            _status = newStatus;
        }
    }
}
