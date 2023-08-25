using Application.Common.Interfaces;
using Azure.Messaging.ServiceBus;
using Domain.Products;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedKernel;
using SharedKernel.IntegrationEvents;

namespace Infastructure.Services
{
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public class ServiceBusMessageProcessor : IServiceBusMessageProcessor
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        private const string ProductProcessorTopicName = "testtopic";
        private const string ProductProcessorSubscriptionName = "testsub";
        private readonly ILogger _logger;
        private readonly ServiceBusProcessor _messageProcessor;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly IServiceProvider _serviceProvider;

        public ServiceBusMessageProcessor(
            IConfiguration configuration,
            ILogger<ServiceBusMessageProcessor> logger,
            IServiceProvider serviceProvider
         )
        {
            string serviceBusConnectionString = configuration["ServiceBusConnection"]!;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _serviceBusClient = new ServiceBusClient(serviceBusConnectionString);
            _messageProcessor = _serviceBusClient.CreateProcessor(ProductProcessorTopicName, ProductProcessorSubscriptionName);
        }

        public async Task Start()
        {
            _logger.LogInformation("Processing Started");
            _messageProcessor.ProcessMessageAsync += OnProductCreatedEventReceived;
            _messageProcessor.ProcessErrorAsync += ErrorHandler;
            await _messageProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            _logger.LogInformation("Processing Stopped");
            await _messageProcessor.StartProcessingAsync();
            await _messageProcessor.DisposeAsync();
            await _serviceBusClient.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError("Error processing Message");
            return Task.CompletedTask;
        }

        private async Task OnProductCreatedEventReceived(ProcessMessageEventArgs args)
        {
            _logger.LogInformation("Event Received");
            var productCreatedIntegrationEvent = JsonConvert.DeserializeObject<ProductCreatedIntegrationEvent>(args.Message.Body.ToString());
            Console.WriteLine(productCreatedIntegrationEvent);
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (productCreatedIntegrationEvent != null)
            {
                var product = new Product(
                   new ProductId(Guid.NewGuid()),
                   productCreatedIntegrationEvent.Name,
                   new Money(productCreatedIntegrationEvent.Currency, productCreatedIntegrationEvent.Amount)
                );

                await dbContext.Products.AddAsync(product);
                await dbContext.SaveChangesAsync();
            }

        }

    }
}
