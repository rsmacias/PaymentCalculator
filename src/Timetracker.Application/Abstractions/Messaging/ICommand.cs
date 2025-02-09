using Acme.Timetracker.Domain.Abstractions;
using MediatR;

namespace Acme.Timetracker.Application.Abstractions.Messaging;

public interface IBaseCommand { }

/// <summary>
/// For the Commands that does not return any data result.
/// </summary>
public interface ICommand : IRequest<Result>, IBaseCommand
{
}

/// <summary>
/// For the Commands that return data result.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{

}