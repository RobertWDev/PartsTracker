using FluentValidation;
using PartsTracker.Modules.Parts.Domain.Parts;

namespace PartsTracker.Modules.Parts.Application.Parts.UpdatePart;

internal sealed class UpdatePartCommandValidator : AbstractValidator<UpdatePartCommand>
{
    private readonly IPartsRepository _partsRepository;
    public UpdatePartCommandValidator(IPartsRepository partsRepository)
    {
        _partsRepository = partsRepository;
        RuleFor(x => x.PartNumber)
            .NotEmpty().WithMessage("Part number is required.")
            .MaximumLength(50).WithMessage("Part number must not exceed 50 characters.")
            .MustAsync(BeExistingPartNumber).WithMessage("Part number must exist.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");
        RuleFor(x => x.QuantityOnHand)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity on hand must be zero or greater.");
        RuleFor(x => x.LocationCode)
            .NotEmpty().WithMessage("Location code is required.")
            .MaximumLength(20).WithMessage("Location code must not exceed 20 characters.");
        RuleFor(x => x.LastStockTake)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Last stock take cannot be in the future.")
            .When(x => x.LastStockTake.HasValue);
    }
    private async Task<bool> BeExistingPartNumber(string partNumber, CancellationToken cancellationToken)
    {
        return await _partsRepository.GetAsync(partNumber, cancellationToken) != null;
    }
}
