using SRS.Interfaces;
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

        public UpdateService(IClientRepository clientRepo, ISessionRepository sessionRepo, ITrainerRepository trainerRepo)
        {
            _clientRepository = clientRepo;
            _sessionRepository = sessionRepo;
            _trainerRepository = trainerRepo;
        }
    }
}
