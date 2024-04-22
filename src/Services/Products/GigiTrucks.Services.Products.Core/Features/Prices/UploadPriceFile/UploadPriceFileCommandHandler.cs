using System.Globalization;
using CsvHelper;
using FluentValidation;
using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.DTOs.Prices;
using GigiTrucks.Services.Products.Core.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Prices.UploadPriceFile;

internal sealed class UploadPriceFileCommandHandler(
    ProductsDbContext dbContext,
    IValidator<UploadPriceFileCommand> validator)
    : IRequestHandler<UploadPriceFileCommand, OneOf<Success, Error<string>>>
{
    public async Task<OneOf<Success, Error<string>>> Handle(
        UploadPriceFileCommand request, 
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Error<string>(validationResult.Errors.ToString()!);
        }
        
        var uploadedPrices = ParsePriceFile(request.File);
        if (uploadedPrices is null || !uploadedPrices.Any())
        {
            return new Error<string>("Can't process uploaded file.");
        }

        try
        {
            var prices = uploadedPrices.Adapt<Price>();
            await dbContext.AddRangeAsync(prices, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return new Error<string>("Can't process uploaded file.");
        }
        return new Success();
    }

    private static IEnumerable<UploadedPriceDto>? ParsePriceFile(IFormFile file)
    {
        var reader = new StreamReader(file.OpenReadStream());
        return new CsvReader(reader, CultureInfo.InvariantCulture)
            .GetRecords<UploadedPriceDto>();
    }
}