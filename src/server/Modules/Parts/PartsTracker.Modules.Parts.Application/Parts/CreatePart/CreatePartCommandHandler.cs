using PartsTracker.Modules.Parts.Application.Abstractions.Data;
using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Shared.Application.Messaging;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.Application.Parts.CreatePart;

internal sealed class CreatePartCommandHandler(IPartsRepository partsRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreatePartCommand, string>
{
    public async Task<Result<string>> Handle(CreatePartCommand request, CancellationToken cancellationToken)
    {
        if (await partsRepository.ExistsByPartNumberAsync(request.PartNumber, cancellationToken))
        {
            return Result.Failure<string>(PartErrors.Exists(request.PartNumber));
        }

        var part = Part.Create(
            request.PartNumber,
            request.Description,
            request.QuantityOnHand,
            request.LocationCode
        );

        partsRepository.Insert(part);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return part.PartNumber;
    }
}
