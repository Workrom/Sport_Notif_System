using SRS.CustomEventArgs;
using SRS.Interfaces;
using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services
{
    public class RegisterService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISessionRepository _sessionRepository;

        public event EventHandler<SessionFullEventArgs> SessionFull;
        public event EventHandler<ClientRegisteredEventArgs> ClientRegistered;

        public RegisterService(IClientRepository clientRepo, ISessionRepository sessionRepo)
        {
            _clientRepository = clientRepo;
            _sessionRepository = sessionRepo;
        }

        public void RegisterClient(Client client, TrainingSession session)
        {
            // Check if session has space or if client is VIP
            if (session.Clients.Count >= session.Capacity && !client.isVIP)
            {
                Console.WriteLine("Session full.");
                return;
            }

            // Add client
            session.Clients.Add(client);
            OnClientRegistered(new ClientRegisteredEventArgs(client, session));

            // Notify if session is full
            if (session.Clients.Count == session.Capacity)
            {
                OnSessionFull(new SessionFullEventArgs(session));
            }
        }
        protected virtual void OnClientRegistered(ClientRegisteredEventArgs e)
        {
            ClientRegistered?.Invoke(this, e);
        }

        protected virtual void OnSessionFull(SessionFullEventArgs e)
        {
            SessionFull?.Invoke(this, e);
        }
    }
}
