using Spectre.Console;
using SRS.CustomEventArgs;
using SRS.Interfaces;
using SRS.Models;

namespace SRS.Services
{
    public class RemoveService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITrainerRepository _trainerRepository;

        public event EventHandler<ClientEventArgs>? ClientRemoved;
        public event EventHandler<TrainerEventArgs>? TrainerRemoved;
        public event EventHandler<SessionEventArgs>? SessionRemoved;

        public RemoveService(IClientRepository clientRepo, ITrainerRepository trainerRepo, ISessionRepository sessionRepo)
        {
            _clientRepository = clientRepo;
            _trainerRepository = trainerRepo;
            _sessionRepository = sessionRepo;
        }
        public void RemoveClient(Client client, TrainingSession session)
        {
            try
            {
                session.Clients.Remove(client);
                _clientRepository.RemoveClient(client);
                OnClientRemoved(new ClientEventArgs(null, client, session));
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"Exception has occured when removing Client {client.Name}: {ex.ToString()}");
            }
        }
        public void RemoveTrainer(Trainer trainer)
        {
            try
            {
                _trainerRepository.RemoveTrainer(trainer);
                OnTrainerRemoved(new TrainerEventArgs(null, trainer));
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"Exception has occured when removing Trainer {trainer.Name}: {ex.ToString()}");
            }
        }
        public void RemoveSession(TrainingSession session, Trainer trainer)
        {
            try
            {
                trainer.Sessions.Remove(session);
                _sessionRepository.RemoveSession(session);
                OnSessionRemoved(new SessionEventArgs(null, session));
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"Exception has occured when removing Session {session.Type}: {ex.ToString()}");
            }
        }
        protected virtual void OnClientRemoved(ClientEventArgs e)
        {
            ClientRemoved?.Invoke(this, e);
        }
        protected virtual void OnTrainerRemoved(TrainerEventArgs e)
        {
            TrainerRemoved?.Invoke(this, e);
        }
        protected virtual void OnSessionRemoved(SessionEventArgs e)
        {
            SessionRemoved?.Invoke(this, e);
        }
    }
}
