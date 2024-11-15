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

        public event EventHandler<SessionFullEventArgs>? SessionFull;
        public event EventHandler<ClientEventArgs>? ClientRegistered;
        public event EventHandler<TrainerEventArgs>? TrainerRegistered;
        public event EventHandler<SessionEventArgs>? SessionRegistered;

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
                    _clientRepository.AddClient(client);
                    OnClientRegistered(new ClientEventArgs(client, session));
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
                _clientRepository.AddClient(client);
                OnClientRegistered(new ClientEventArgs(client, session));
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
            OnTrainerRegistered(new TrainerEventArgs(trainer));
        }
        public void RegisterSession(TrainingSession session, List<Client>? clients, Trainer trainer)
        {
            session.Clients = clients ?? session.Clients;
            _sessionRepository.AddSession(session);
            OnSessionRegistered(new SessionEventArgs(session, trainer));
        }
        protected virtual void OnSessionRegistered(SessionEventArgs e)
        {
            SessionRegistered?.Invoke(this, e);
        }
        protected virtual void OnTrainerRegistered(TrainerEventArgs e)
        {
            TrainerRegistered?.Invoke(this, e);
        }
        protected virtual void OnClientRegistered(ClientEventArgs e)
        {
            ClientRegistered?.Invoke(this, e);
        }

        protected virtual void OnSessionFull(SessionFullEventArgs e)
        {
            SessionFull?.Invoke(this, e);
        }
    }
}
