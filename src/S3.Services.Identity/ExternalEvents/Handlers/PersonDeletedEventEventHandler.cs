using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using S3.Services.Identity.Repositories;

namespace S3.Services.Identity.ExternalEvents.Handlers
{
    public class PersonDeletedEventHandler : IEventHandler<PersonDeletedEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<PersonDeletedEventHandler> _logger;

        public PersonDeletedEventHandler(IUserRepository userRepository, ILogger<PersonDeletedEventHandler> logger)
            => (_userRepository, _logger) = (userRepository, logger);

        public async Task HandleAsync(PersonDeletedEvent @event, ICorrelationContext context)
        {
            var person = await _userRepository.GetAsync(@event.Id);
            if (!(person is null))
            { 
                await _userRepository.RemoveAsync(@event.Id);
                _logger.LogInformation($"Deleted user: '{person.Username}' in the identity db, after a successful removal in the registration db.");
            }
            else
            {
                _logger.LogInformation($"The deleted user with id: '{@event.Id}' was never signed up, so could not be found in the identity db.");
            }
        }
    }
}