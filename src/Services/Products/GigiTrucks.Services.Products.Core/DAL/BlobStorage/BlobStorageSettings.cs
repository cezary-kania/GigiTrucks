namespace GigiTrucks.Services.Products.Core.DAL.BlobStorage;

public class BlobStorageSettings
{
    public string StorageAccount { get; set; } = null!;
    public string BlobHost { get; set; } = null!;
    public string Key { get; set; } = null!;
    public string ProductImagesContainerName { get; set; } = null!;
    public string BlobUri => $"{BlobHost}/{StorageAccount}";
}