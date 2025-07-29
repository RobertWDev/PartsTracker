using MediatR;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Shared.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
