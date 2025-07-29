using FluentValidation;
using PartsTracker.Modules.Parts.Domain.Parts;

namespace PartsTracker.Modules.Parts.Application.Parts.CreatePart;

internal sealed class CreatePartCommandValidator : AbstractValidator<CreatePartCommand>
{
    private readonly IPartsRepository _partsRepository;

    public CreatePartCommandValidator(IPartsRepository partsRepository)
    {
        _partsRepository = partsRepository;

        RuleFor(x => x.PartNumber)
            .NotEmpty().WithMessage("Part number is required.")
            .MaximumLength(50).WithMessage("Part number must not exceed 50 characters.")
            .MustAsync(BeUniquePartNumber).WithMessage("Part number must be unique.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
        RuleFor(x => x.QuantityOnHand)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity on hand must be zero or greater.");
        RuleFor(x => x.LocationCode)
            .NotEmpty().WithMessage("Location code is required.")
            .MaximumLength(20).WithMessage("Location code must not exceed 20 characters.");
    }

    private async Task<bool> BeUniquePartNumber(string partNumber, CancellationToken cancellationToken)
    {
        return !await _partsRepository.ExistsByPartNumberAsync(partNumber, cancellationToken);
    }
}
