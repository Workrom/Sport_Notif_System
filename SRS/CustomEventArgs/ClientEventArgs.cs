using SRS.Models;

namespace SRS.CustomEventArgs
{
    public class ClientEventArgs(Client? previousclient, Client client, TrainingSession session) : EventArgs
    {
        public Client? previousClient { get; } = previousclient;
        public Client Client { get; } = client;
        public TrainingSession Session { get; } = session;
    }
}
