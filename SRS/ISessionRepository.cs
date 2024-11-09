using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS
{
    public interface ISessionRepository
    {
        TrainingSession GetSessionByType(string type);
        void AddSession(TrainingSession session);
    }
}
