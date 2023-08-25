namespace Application.Common.Interfaces
{
    public interface IServiceBusMessageProcessor
    {
        Task Start();
        Task Stop();
    }
}
