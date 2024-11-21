using SRS.Models;

namespace SRS.CustomEventArgs
{
    public class SessionEventArgs(TrainingSession? previousSession, TrainingSession session): EventArgs
    {
        public TrainingSession previousSession { get; } = previousSession;
        public TrainingSession Session { get; } = session;
    }
}
