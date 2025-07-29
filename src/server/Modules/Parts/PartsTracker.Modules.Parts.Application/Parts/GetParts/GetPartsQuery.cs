using PartsTracker.Modules.Parts.Application.Parts.GetPart;
using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Parts.Application.Parts.GetParts;
public sealed record GetPartsQuery() : IQuery<IEnumerable<PartDto>>;
