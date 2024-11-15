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
        public void UpdateClient(Client updatedClient)
        {
            var client = _clients.FirstOrDefault(c => c.Name == updatedClient.Name);

            if (client != null)
            {
                client.Name = updatedClient.Name;
                client.Age = updatedClient.Age;
                client.isVIP = updatedClient.isVIP;
            }
            else
            {
                throw new Exception("Client not found");
            }
        }
    }
}
