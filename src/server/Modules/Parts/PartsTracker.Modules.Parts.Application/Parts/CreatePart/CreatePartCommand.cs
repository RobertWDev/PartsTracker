using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Parts.Application.Parts.CreatePart;
public sealed record CreatePartCommand(string PartNumber, string Description, int QuantityOnHand, string LocationCode) : ICommand<string>;
