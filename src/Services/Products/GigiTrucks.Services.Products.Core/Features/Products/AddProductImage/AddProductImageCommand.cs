using MediatR;
using Microsoft.AspNetCore.Http;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Products.AddProductImage;

public record AddProductImageCommand(Guid ProductId, IFormFile File) : IRequest<OneOf<Success,NotFound,Error<string>>>;