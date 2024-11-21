using SRS.Models;

namespace SRS.Interfaces.InMemory
{
    public class InMemorySessionRepository : ISessionRepository
    {
        private readonly List<TrainingSession> _sessions = new List<TrainingSession>();
        public IEnumerable<TrainingSession> GetAllSessions()
        {
            return _sessions.AsReadOnly();
        }
        public TrainingSession GetSessionByType(string type)
        {
            return _sessions.FirstOrDefault(s => s.Type == type);
        }
        public void AddSession(TrainingSession session)
        {
            _sessions.Add(session);
        }
        public void RemoveSession(TrainingSession session)
        {
            _sessions.Remove(session);
        }
        public void UpdateSession(TrainingSession currentSession, TrainingSession updatedSession)
        {
            currentSession = _sessions.FirstOrDefault(s => s.Type == currentSession.Type);

            if (currentSession != null)
            {
                currentSession.Type = updatedSession.Type;
                currentSession.Capacity = updatedSession.Capacity;
                currentSession.Clients = updatedSession.Clients;
            }
            else
            {
                throw new Exception("Session not found");
            }
        }
        public int GetCount()
        {
            return _sessions.Count;
        }
    }
}
