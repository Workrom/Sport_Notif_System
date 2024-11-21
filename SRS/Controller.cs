using Spectre.Console;
using SRS.Handlers;
using SRS.Interfaces.InMemory;
using SRS.Models;
using SRS.Services;

namespace SRS
{
    public class Controller
    {
        private InMemoryClientRepository _clientRepository;
        private InMemoryTrainerRepository _trainerRepository;
        private InMemorySessionRepository _sessionRepository;

        private RegisterService _registerService;
        private RemoveService _removeService;
        private UpdateService _updateService;

        private HandleManager _handleManager;
        public Controller()
        {
            _clientRepository = new InMemoryClientRepository();
            _trainerRepository = new InMemoryTrainerRepository();
            _sessionRepository = new InMemorySessionRepository();

            _registerService = new RegisterService(_clientRepository, _trainerRepository, _sessionRepository);
            _removeService = new RemoveService(_clientRepository, _trainerRepository, _sessionRepository);
            _updateService = new UpdateService(_clientRepository, _trainerRepository, _sessionRepository);

            _handleManager = new HandleManager();


            _registerService.SessionFull += _handleManager.OnSessionFullHandler;

            _registerService.ClientRegistered += _handleManager.OnClientRegisteredHandler;
            _registerService.TrainerRegistered += _handleManager.OnTrainerRegisteredHandler;
            _registerService.SessionRegistered += _handleManager.OnSessionRegisteredHandler;

            _removeService.ClientRemoved += _handleManager.OnClientRemovedHandler;
            _removeService.TrainerRemoved += _handleManager.OnTrainerRemovedHandler;
            _removeService.SessionRemoved += _handleManager.OnSessionRemovedHandler;

            _updateService.ClientUpdated += _handleManager.OnClientUpdated;
            _updateService.TrainerUpdated += _handleManager.OnTrainerUpdated;
            _updateService.SessionUpdated += _handleManager.OnSessionUpdated;
        }

        public void AddTrainer(string name, int age, List<TrainingSession>? sessions)
        {
            Trainer trainer = new Trainer { Name = name, Age = age };
            _registerService.RegisterTrainer(trainer, sessions);
        }
        public void RemoveTrainer(Trainer trainer)
        {
            _removeService.RemoveTrainer(trainer);
        }
        public void UpdateTrainer(Trainer trainer, string name, int age)
        {
            Trainer updatedTrainer = new Trainer { Name = name, Age = age };
            _updateService.UpdateTrainer(trainer, updatedTrainer);
        }

        public void AddSession(string type, int capacity, Trainer trainer, List<Client>? clients)
        {
            TrainingSession session = new TrainingSession { Type = type, Capacity = capacity };
            _registerService.RegisterSession(session, clients, trainer);
        }
        public void RemoveSession(TrainingSession session, Trainer trainer)
        {
            _removeService.RemoveSession(session, trainer);
        }
        public void UpdateSession(TrainingSession session, string type, int capacity)
        {
            if (session.Status.ToString() == "FREE")
            {
                if (capacity < session.Clients.Count)
                {
                    AnsiConsole.MarkupLine("[deeppink4_2]Cannot update training session with capacity less than previous session clients count![/]");
                }
                else
                {
                    TrainingSession updatedSession = new TrainingSession { Type = type, Capacity = capacity , Clients = session.Clients, VIPClient = session.VIPClient};
                    _updateService.UpdateSession(session, updatedSession);
                }
            }
            else if (session.Status.ToString() == "FULL")
            {
                AnsiConsole.MarkupLine("[deeppink4_2]Cannot update FULL training session![/]");
            }
            else if (session.Status.ToString() == "BUSY")
            {
                AnsiConsole.MarkupLine("[deeppink4_2]Cannot update already ongoing training session![/]");
            }
            else if (session.Status.ToString() == "DONE")
            {
                AnsiConsole.MarkupLine("[deeppink4_2]Cannot update already DONE training session, it will be deleted automaticly soon![/]");
            }
        }
        public void AddClient(string name, int age, bool isvip, TrainingSession session)
        {
            if(session.Status.ToString() == "FREE")
            {
                Client client = new Client { Name = name, Age = age, isVIP = isvip };
                _registerService.RegisterClient(client, session);
            }
            else if (session.Status.ToString() == "FULL")
            {
                if(isvip && session.VIPClient == null)
                {
                    Client client = new Client { Name = name, Age = age, isVIP = isvip };
                    _registerService.RegisterClient(client, session);
                }
                else if (isvip && session.VIPClient != null)
                {
                    AnsiConsole.MarkupLine("[deeppink4_2]Cannot register VIP client the reserve slot is already occupied![/]");
                }
                else if (!isvip)
                {
                    AnsiConsole.MarkupLine("[deeppink4_2]Cannot register session is already FULL![/]");

                }
            }
            else if (session.Status.ToString() == "BUSY")
            {
                AnsiConsole.MarkupLine("[deeppink4_2]Cannot register to already ongoing training session![/]");
            }
            else if (session.Status.ToString() == "DONE")
            {
                AnsiConsole.MarkupLine("[deeppink4_2]Cannot register to DONE training session, it will be deleted automaticly soon![/]");
            }
        }
        public void RemoveClient(Client client, TrainingSession session)
        {
            _removeService.RemoveClient(client, session);
        }

        public void UpdateClient(Client client, string name, int age, bool isvip, TrainingSession session)
        {
            Client updatedClient = new Client { Name = name, Age = age, isVIP = isvip };
            _updateService.UpdateClient(client, updatedClient, session);
        }

        public IEnumerable<T> GetListOf<T>() where T : class
        {
            if (typeof(T) == typeof(Client))
            {
                return _clientRepository.GetAllClients() as IEnumerable<T>;
            }
            if (typeof(T) == typeof(Trainer))
            {
                return _trainerRepository.GetAllTrainers() as IEnumerable<T>;
            }
            if (typeof(T) == typeof(TrainingSession))
            {
                return _sessionRepository.GetAllSessions() as IEnumerable<T>;
            }

            throw new InvalidOperationException($"Unsupported type {typeof(T).Name}");
        }

    }
}
