using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace GigiTrucks.Services.Products.Core.Features.Prices.UploadPriceFile;

public class PriceFileValidator : AbstractValidator<IFormFile>
{
    public PriceFileValidator()
    {
        RuleFor(x => Path.GetExtension(x.FileName))
            .Equal("csv")
            .WithMessage("Must be csv file");
        
        RuleFor(x => x.ContentType.ToLower())
            .Equal("text/csv")
            .WithMessage("Invalid content type");
    }
}