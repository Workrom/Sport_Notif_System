using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Models
{
    public class TrainingSession
    {
        public required string Type { get; set; }
        public required int Capacity { get; set; }
        public required string Status { get; set; }
        public List<Client> Clients { get; set; } = new List<Client>();
    }
}
