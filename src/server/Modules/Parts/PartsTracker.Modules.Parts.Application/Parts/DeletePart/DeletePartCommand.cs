using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Parts.Application.Parts.DeletePart;
public sealed record DeletePartCommand(string PartNumber) : ICommand;
