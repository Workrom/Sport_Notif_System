using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS
{
    public class TrainingSession
    {
        public string Type { get; set; }
        public int Capacity { get; set; }
        List<Client> clients { get; set; } = new List<Client>();
    }
}
