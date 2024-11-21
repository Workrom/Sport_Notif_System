using SRS.Models;

namespace SRS.Interfaces
{
    public interface ISessionRepository
    {
        TrainingSession GetSessionByType(string type);
        void AddSession(TrainingSession session);
        void RemoveSession(TrainingSession session);
        void UpdateSession(TrainingSession currentSession, TrainingSession updatedSession);
    }
}
