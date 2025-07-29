using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.Domain.Parts;

public static class PartErrors
{
    public static Error NotFound(string partNumber) =>
        Error.NotFound("Parts.NotFound", $"The part with the part number {partNumber} not found");

    public static Error Exists(string partNumber) =>
        Error.Conflict("Parts.Exists", $"The part with the part number {partNumber} already exists");
}
