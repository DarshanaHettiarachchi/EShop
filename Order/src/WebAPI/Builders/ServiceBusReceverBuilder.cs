using Application.Common.Interfaces;

namespace WebAPI.Builders
{
    public static class ServiceBusReceiverBuilder
    {
        private static IServiceBusMessageProcessor? ServiceBusMessageProcessor { get; set; }
        public static IApplicationBuilder UseServiceBusMessageProcessor(this IApplicationBuilder app)
        {
            ServiceBusMessageProcessor = app.ApplicationServices.GetService<IServiceBusMessageProcessor>()!;

            var hostApplicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLifetime?.ApplicationStarted.Register(Start);

            hostApplicationLifetime?.ApplicationStopped.Register(Stop);

            return app;
        }

        private static void Start()
        {
            ServiceBusMessageProcessor?.Start();
        }

        private static void Stop()
        {
            ServiceBusMessageProcessor?.Stop();
        }
    }
}
