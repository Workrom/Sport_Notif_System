using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS
{    
    public class Trainer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        List<TrainingSession> Sessions { get; set; } = new List<TrainingSession>();
    }
}
