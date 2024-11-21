using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRS.Models;

namespace SRS.Interfaces.InMemory
{
    public class InMemoryClientRepository : IClientRepository
    {
        private readonly List<Client> _clients = new List<Client>();
        public IEnumerable<Client> GetAllClients()
        {
            return _clients.AsReadOnly();
        }
        public Client GetClientByName (string name)
        {
            return _clients.FirstOrDefault(c => c.Name == name);
        }
        public void AddClient (Client client)
        {
            _clients.Add(client);
        }
        public void RemoveClient (Client client)
        {
            _clients.Remove(client);
        }
        public void UpdateClient(Client currentClient, Client updatedClient)
        {
            currentClient = _clients.FirstOrDefault(c => c.Name == currentClient.Name);

            if (currentClient != null)
            {
                currentClient.Name = updatedClient.Name;
                currentClient.Age = updatedClient.Age;
                currentClient.isVIP = updatedClient.isVIP;
            }
            else
            {
                throw new Exception("Client not found");
            }
        }
        public int GetCount()
        {
            return _clients.Count();
        }
    }
}
