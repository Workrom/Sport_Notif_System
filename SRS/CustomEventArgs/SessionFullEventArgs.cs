using SRS.Models;

namespace SRS.CustomEventArgs
{
    public class SessionFullEventArgs(TrainingSession session): EventArgs
    {
        public TrainingSession Session { get; } = session;
    }
}
