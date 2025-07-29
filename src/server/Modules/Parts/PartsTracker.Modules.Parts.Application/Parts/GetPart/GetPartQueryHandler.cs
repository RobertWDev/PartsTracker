using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Shared.Application.Messaging;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.Application.Parts.GetPart;

internal sealed class GetPartQueryHandler(IPartsRepository partsRepository) : IQueryHandler<GetPartQuery, PartDto?>
{
    public async Task<Result<PartDto?>> Handle(GetPartQuery request, CancellationToken cancellationToken)
    {
        Part? part = await partsRepository.GetAsync(request.PartNumber, cancellationToken);

        if (part is null)
        {
            return Result.Failure<PartDto?>(PartErrors.NotFound(request.PartNumber));
        }

        return new PartDto(
            part.PartNumber,
            part.Description,
            part.QuantityOnHand,
            part.LocationCode,
            part.LastStockTake
        );
    }
}
