namespace GigiTrucks.Services.Products.Core.DAL.BlobStorage;

public class BlobStorageSettings
{
    public string ProductImagesContainerName { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;
}