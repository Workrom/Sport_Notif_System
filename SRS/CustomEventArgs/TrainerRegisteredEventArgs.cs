using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.CustomEventArgs
{
    public class TrainerRegisteredEventArgs(Trainer trainer) : EventArgs
    {
        public Trainer Trainer { get; } = trainer;
    }
}
