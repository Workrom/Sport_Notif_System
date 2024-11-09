using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.CustomEventArgs
{
    public class ClientRegisteredEventArgs(Client client, TrainingSession session) : EventArgs
    {
        public Client Client { get; } = client;
        public TrainingSession Session { get; } = session;
    }
}
