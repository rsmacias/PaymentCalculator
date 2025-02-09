using Acme.Timetracker.Domain.Abstractions;
using MediatR;

namespace Acme.Timetracker.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> 
    where TQuery : IQuery<TResponse>
{

}