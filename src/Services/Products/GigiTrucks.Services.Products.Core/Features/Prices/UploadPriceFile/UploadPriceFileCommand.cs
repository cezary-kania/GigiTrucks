using MediatR;
using Microsoft.AspNetCore.Http;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Prices.UploadPriceFile;

public record UploadPriceFileCommand(IFormFile File) 
    : IRequest<OneOf<Success, Error<string>>>;