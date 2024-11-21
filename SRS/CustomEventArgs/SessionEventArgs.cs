using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.CustomEventArgs
{
    public class SessionEventArgs(TrainingSession? previousSession, TrainingSession session): EventArgs
    {
        public TrainingSession previousSession { get; } = previousSession;
        public TrainingSession Session { get; } = session;
    }
}
