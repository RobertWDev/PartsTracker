using PartsTracker.Modules.Parts.Application.Abstractions.Data;
using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Shared.Application.Messaging;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.Application.Parts.UpdatePart;

internal sealed class UpdatePartCommandHandler(IPartsRepository partsRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdatePartCommand>
{
    public async Task<Result> Handle(UpdatePartCommand request, CancellationToken cancellationToken)
    {
        Part? part = await partsRepository.GetAsync(request.PartNumber, cancellationToken);

        if (part is null)
        {
            return Result.Failure(PartErrors.NotFound(request.PartNumber));
        }

        part.UpdateDescription(request.Description);
        part.UpdateQuantityOnHand(request.QuantityOnHand);
        part.UpdateLocationCode(request.LocationCode);
        part.UpdateLastStockTake(request.LastStockTake);

        partsRepository.Update(part);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
