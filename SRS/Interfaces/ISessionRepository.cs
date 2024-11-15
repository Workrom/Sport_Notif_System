using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRS.Models;

namespace SRS.Interfaces
{
    public interface ISessionRepository
    {
        TrainingSession GetSessionByType(string type);
        void AddSession(TrainingSession session);
        void RemoveSession(TrainingSession session);
        void UpdateSession(TrainingSession session);
    }
}
