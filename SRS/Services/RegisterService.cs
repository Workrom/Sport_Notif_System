using Spectre.Console;
using SRS.CustomEventArgs;
using SRS.Interfaces;
using SRS.Models;

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

        public RegisterService(IClientRepository clientRepo, ITrainerRepository trainerRepo, ISessionRepository sessionRepo)
        {
            _clientRepository = clientRepo;
            _trainerRepository = trainerRepo;
            _sessionRepository = sessionRepo;
        }
        public void RegisterClient(Client client, TrainingSession session)
        {
            if (session.Clients.Count >= session.Capacity && !(client.isVIP))
            {
                OnSessionFull(new SessionFullEventArgs(session));
                return;
            }
            else if (client.isVIP)
            {
                if (session.Clients.Count >= session.Capacity)
                {
                    if (session.VIPClient != null)
                    {
                        AnsiConsole.MarkupLine($"VIP reserve slot is occupied by {session.VIPClient.Name}");
                    }
                    else
                    {
                        session.VIPClient = client;
                        _clientRepository.AddClient(client);
                        OnClientRegistered(new ClientEventArgs(null, client, session));
                    }
                }
                else
                {
                    session.Clients.Add(client);
                    _clientRepository.AddClient(client);
                    OnClientRegistered(new ClientEventArgs(null, client, session));
                }
            }
            else
            {
                session.Clients.Add(client);
                _clientRepository.AddClient(client);
                OnClientRegistered(new ClientEventArgs(null, client, session));
            }
        }
        public void RegisterTrainer(Trainer trainer, List<TrainingSession>? sessions)
        {
            trainer.Sessions = sessions ?? trainer.Sessions;
            _trainerRepository.AddTrainer(trainer);
            OnTrainerRegistered(new TrainerEventArgs(null, trainer));
        }
        public void RegisterSession(TrainingSession session, List<Client>? clients, Trainer trainer)
        {
            session.Clients = clients ?? session.Clients;
            trainer.Sessions.Add(session);
            _sessionRepository.AddSession(session);
            OnSessionRegistered(new SessionEventArgs(null, session));
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
