using Acme.Timetracker.Domain.Abstractions;
using MediatR;

namespace Acme.Timetracker.Application.Abstractions.Messaging;

/// <summary>
/// Handler for the Commands that does not return any data result.
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result> 
        where TCommand : ICommand
{

}

/// <summary>
/// Handler for the Commands that return data result.
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> 
    where TCommand : ICommand<TResponse>
{

}