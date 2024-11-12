using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Interfaces.InMemory
{
    public class InMemorySessionRepository : ISessionRepository
    {
        private readonly List<TrainingSession> _sessions = new List<TrainingSession>();
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
        public void UpdateSession(TrainingSession updatedSession)
        {
            var session = _sessions.FirstOrDefault(s => s.Type == updatedSession.Type);

            if (session != null)
            {
                session.Type = updatedSession.Type;
                session.Capacity = updatedSession.Capacity;
                session.Clients = updatedSession.Clients;
            }
            else
            {
                throw new Exception("Trainer not found");
            }
        }
    }
}
