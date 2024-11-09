using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Models
{
    public class Client
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
        public required bool isVIP { get; set; }
    }
}
