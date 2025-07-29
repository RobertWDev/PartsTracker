using PartsTracker.Modules.Parts.Application.Abstractions.Data;
using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Shared.Application.Messaging;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.Application.Parts.DeletePart;

internal sealed class DeletePartCommandHandler(IPartsRepository partsRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeletePartCommand>
{
    public async Task<Result> Handle(DeletePartCommand request, CancellationToken cancellationToken)
    {
        Part? part = await partsRepository.GetAsync(request.PartNumber, cancellationToken);

        if (part is null)
        {
            return Result.Failure(PartErrors.NotFound(request.PartNumber));
        }

        part.MarkAsDeleted();

        partsRepository.Update(part);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
