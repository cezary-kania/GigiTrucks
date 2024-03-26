using Azure.Storage.Blobs;
using GigiTrucks.Services.Products.Core.DAL.BlobStorage;
using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OneOf;
using OneOf.Types;

namespace GigiTrucks.Services.Products.Core.Features.Products.AddProductImage;

internal sealed class AddProductImageCommandHandler(
    ProductsDbContext dbContext,
    BlobServiceClient blobServiceClient,
    IOptionsMonitor<BlobStorageSettings> blobStorageSettings) 
    : IRequestHandler<AddProductImageCommand, OneOf<Success, NotFound, Error<string>>>
{
    private const int MaxImagesPerProductCount = 5;

    public async Task<OneOf<Success, NotFound, Error<string>>> Handle(
        AddProductImageCommand request, 
        CancellationToken cancellationToken)
    {
        var product = await GetProduct(request, cancellationToken);
        
        if (product is null)
        {
            return new NotFound();
        }

        if (product.Images.Count >= MaxImagesPerProductCount)
        {
            return new Error<string>($"Can't upload more than {MaxImagesPerProductCount} images.");
        }

        var blobClient = await UploadImage(request, cancellationToken);

        await CreateProductImage(product, blobClient, cancellationToken);

        return new Success();
    }

    private async Task<Product?> GetProduct(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        return product;
    }

    private async Task CreateProductImage(Product product, BlobClient blobClient, CancellationToken cancellationToken)
    {
        product.Images.Add(new Image
        {
            Uri = blobClient.Uri.AbsoluteUri,
            Name = blobClient.Name,
            DisplayOrder = product.Images.Count + 1
        });
        
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<BlobClient> UploadImage(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(
            blobStorageSettings.CurrentValue.ProductImagesContainerName);
        
        await using var dataStream = request.File.OpenReadStream();
        var fileName = Guid.NewGuid().ToString();
        var extension = Path.GetExtension(request.File.FileName);
        var blobClient = blobContainerClient.GetBlobClient(fileName + extension);
        await blobClient.UploadAsync(dataStream, cancellationToken);
        
        return blobClient;
    }
}