using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRS.Models;

namespace SRS.Interfaces
{
    public interface IClientRepository
    {
        Client GetClientByName(string name);
        void AddClient(Client client);
        void RemoveClient(Client client);
        void UpdateClient(Client currentClient, Client updatedClient);
    }
}
