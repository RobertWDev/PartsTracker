using MediatR;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Shared.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
