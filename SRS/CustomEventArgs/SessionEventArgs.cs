using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.CustomEventArgs
{
    public class SessionEventArgs(TrainingSession session, Trainer trainer): EventArgs
    {
        public TrainingSession Session { get; } = session;
        public Trainer Trainer { get; } = trainer;
    }
}
