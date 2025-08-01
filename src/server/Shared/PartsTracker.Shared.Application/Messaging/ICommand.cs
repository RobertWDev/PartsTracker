﻿using MediatR;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Shared.Application.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
