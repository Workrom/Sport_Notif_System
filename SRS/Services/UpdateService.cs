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
    public class UpdateService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITrainerRepository _trainerRepository;

        public event EventHandler<ClientEventArgs>? ClientUpdated;
        public event EventHandler<TrainerEventArgs>? TrainerUpdated;
        public event EventHandler<SessionEventArgs>? SessionUpdated;
        public UpdateService(IClientRepository clientRepo, ITrainerRepository trainerRepo, ISessionRepository sessionRepo)
        {
            _clientRepository = clientRepo;
            _trainerRepository = trainerRepo;
            _sessionRepository = sessionRepo;
        }

        public void UpdateClient(Client currentClient,Client updatedClient, TrainingSession session)
        {
            if (!currentClient.isVIP)
            {
                OnClientUpdated(new ClientEventArgs(currentClient, updatedClient, session));
                _clientRepository.UpdateClient(currentClient, updatedClient);
            }
            else
            {
                //session.Clients.Remove(currentClient);  -  controller will handle that
                session.VIPClient = updatedClient;
                OnClientUpdated(new ClientEventArgs(currentClient, updatedClient, session));
                _clientRepository.UpdateClient(currentClient, updatedClient);
            }
        }
        public void UpdateTrainer(Trainer currentTrainer, Trainer updatedTrainer)
        {
            //okay this shi updates currentTrainer with updated and i cant handle correctly-
            //-because they both are the same, so ill just swap places
            //Works, who cares this program will never be used
            OnTrainerUpdated(new TrainerEventArgs(currentTrainer, updatedTrainer));
            _trainerRepository.UpdateTrainer(currentTrainer, updatedTrainer);
            
        }
        public void UpdateSession(TrainingSession currentSession, TrainingSession updatedSession)
        {
            OnSessionUpdated(new SessionEventArgs(currentSession, updatedSession));
            _sessionRepository.UpdateSession(currentSession, updatedSession);
        }
        protected virtual void OnClientUpdated(ClientEventArgs e)
        {
            ClientUpdated?.Invoke(this, e);
        }
        protected virtual void OnTrainerUpdated(TrainerEventArgs e)
        {
            TrainerUpdated?.Invoke(this, e);
        }
        protected virtual void OnSessionUpdated(SessionEventArgs e)
        {
            SessionUpdated?.Invoke(this, e);
        }
    }
}
