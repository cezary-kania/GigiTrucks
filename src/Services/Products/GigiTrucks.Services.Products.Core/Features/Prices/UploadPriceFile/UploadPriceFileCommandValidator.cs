using FluentValidation;

namespace GigiTrucks.Services.Products.Core.Features.Prices.UploadPriceFile;

public class UploadPriceFileCommandValidator : AbstractValidator<UploadPriceFileCommand>
{
    public UploadPriceFileCommandValidator()
    {
        RuleFor(x => x.File)
            .NotEmpty()
            .SetValidator(new PriceFileValidator());
    }
}