using Application.Common.Interfaces;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedKernel.IntegrationEvents;

namespace Infastructure.Services
{
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public class ProductCreatedIntegrationEventPublisherService : IProductCreatedIntegrationEventPublisherService
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        private readonly ILogger _logger;
        private const string TopicName = "testtopic";
        private readonly ServiceBusClient _serviceBusClient;
        public ProductCreatedIntegrationEventPublisherService(
            IConfiguration configuration,
            ILogger<ProductCreatedIntegrationEventPublisherService> logger

        )
        {
            string serviceBusConnectionString = configuration["ServiceBusConnection"]!;
            _logger = logger;
            _serviceBusClient = new ServiceBusClient(serviceBusConnectionString);

        }
        public async void PublishEvent(ProductCreatedIntegrationEvent integrationEvent)
        {

            ServiceBusSender sender = _serviceBusClient.CreateSender(TopicName);

            var jsonIntegrationEvent = JsonConvert.SerializeObject(integrationEvent);

            ServiceBusMessage message = new ServiceBusMessage(jsonIntegrationEvent);


            // send the message
            await sender.SendMessageAsync(message);

            _logger.LogInformation($"Publishing to asb");
        }
    }
}
