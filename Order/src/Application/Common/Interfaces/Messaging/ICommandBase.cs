using MediatR;

namespace Application.Common.Interfaces.Messaging;

#pragma warning disable CA1040 // Avoid empty interfaces
public interface ICommand : IRequest, ICommandBase

{
}

#pragma warning disable S3246 // Generic type parameters should be co/contravariant when possible
public interface ICommand<TResponse> : IRequest<TResponse>, ICommandBase
#pragma warning restore S3246 // Generic type parameters should be co/contravariant when possible
{
}

public interface ICommandBase
{
}
#pragma warning restore CA1040 // Avoid empty interfaces