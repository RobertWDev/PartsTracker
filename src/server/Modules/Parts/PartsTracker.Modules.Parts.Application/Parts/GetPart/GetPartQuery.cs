using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Parts.Application.Parts.GetPart;
public sealed record GetPartQuery(string PartNumber) : IQuery<PartDto?>;
