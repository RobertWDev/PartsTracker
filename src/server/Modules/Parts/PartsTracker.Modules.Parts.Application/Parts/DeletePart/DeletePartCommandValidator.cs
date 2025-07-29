using FluentValidation;
using PartsTracker.Modules.Parts.Domain.Parts;

namespace PartsTracker.Modules.Parts.Application.Parts.DeletePart;

internal sealed class DeletePartCommandValidator : AbstractValidator<DeletePartCommand>
{
    private readonly IPartsRepository _partsRepository;
    public DeletePartCommandValidator(IPartsRepository partsRepository)
    {
        _partsRepository = partsRepository;
        RuleFor(x => x.PartNumber)
            .NotEmpty().WithMessage("Part number is required.")
            .MaximumLength(50).WithMessage("Part number must not exceed 50 characters.")
            .MustAsync(BeExistingPartNumber).WithMessage("Part number must exist.");
    }
    private async Task<bool> BeExistingPartNumber(string partNumber, CancellationToken cancellationToken)
    {
        return await _partsRepository.GetAsync(partNumber, cancellationToken) != null;
    }
}
