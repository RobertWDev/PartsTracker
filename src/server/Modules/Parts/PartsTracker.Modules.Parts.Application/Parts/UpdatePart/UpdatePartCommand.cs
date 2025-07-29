using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Parts.Application.Parts.UpdatePart;
public sealed record UpdatePartCommand(string PartNumber, string Description, int QuantityOnHand, string LocationCode, DateTime? LastStockTake = null) : ICommand;
