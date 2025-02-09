using Acme.Timetracker.Domain.Abstractions;
using MediatR;

namespace Acme.Timetracker.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
