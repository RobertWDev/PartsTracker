namespace PartsTracker.Modules.Parts.Application.Parts.GetPart;

public sealed record PartDto(
    string PartNumber,
    string Description,
    int QuantityOnHand,
    string LocationCode,
    DateTime? LastStockTake = null);
