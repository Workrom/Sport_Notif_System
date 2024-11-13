using Spectre.Console;
using SRS.CustomEventArgs;
using SRS.Interfaces;
using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SRS.Services
{
    public class RegisterService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITrainerRepository _trainerRepository;

        public event EventHandler<SessionFullEventArgs> SessionFull;
        public event EventHandler<ClientRegisteredEventArgs> ClientRegistered;
        public event EventHandler<TrainerRegisteredEventArgs> TrainerRegistered;
        public event EventHandler<SessionRegisteredEventArgs> SessionRegistered;

        public RegisterService(IClientRepository clientRepo, ISessionRepository sessionRepo, ITrainerRepository trainerRepo)
        {
            _clientRepository = clientRepo;
            _sessionRepository = sessionRepo;
            _trainerRepository = trainerRepo;
        }
        public void RegisterClient(Client client, TrainingSession session)
        {
            if (session.Clients.Count == session.Capacity && !(session.VIPClient == null))
            {
                OnSessionFull(new SessionFullEventArgs(session));
                return;
            }
            if (client.isVIP)
            {
                if (session.VIPClient == null)
                {
                    session.VIPClient = client;
                    OnClientRegistered(new ClientRegisteredEventArgs(client, session));
                    _clientRepository.AddClient(client);
                }
                else
                {
                    AnsiConsole.MarkupLine("VIP slot is [red]already occupied.[/]");
                    return;
                }
            }
            else
            {
                session.Clients.Add(client);
                OnClientRegistered(new ClientRegisteredEventArgs(client, session));
                _clientRepository.AddClient(client);
            }

            //*Dont know if i really need this. We gonna find out soon
            //if (session.Clients.Count >= session.Capacity && !client.isVIP)
            //{
            //    AnsiConsole.MarkupLine("Session full.");
            //    return;
            //}          
        }
        public void RegisterTrainer(Trainer trainer, List<TrainingSession>? sessions)
        {
            trainer.Sessions = sessions ?? trainer.Sessions;
            _trainerRepository.AddTrainer(trainer);
            OnTrainerRegistered(new TrainerRegisteredEventArgs(trainer));
        }
        public void RegisterSession(TrainingSession session, List<Client>? clients)
        {
            session.Clients = clients ?? session.Clients;
            _sessionRepository.AddSession(session);
            OnSessionRegistered(new SessionRegisteredEventArgs(session));
        }
        protected virtual void OnSessionRegistered(SessionRegisteredEventArgs e)
        {
            SessionRegistered?.Invoke(this, e);
        }
        protected virtual void OnTrainerRegistered(TrainerRegisteredEventArgs e)
        {
            TrainerRegistered?.Invoke(this, e);
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
