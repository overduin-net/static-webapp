public interface IBlobStorageRepository
{
    Task<IEnumerable<BlobItem>> GetAllBlobsFromDirectory(string containerName);
    Task<BlobContentInfo> StoreBlob(string containerName, string blobName, Stream content);
    Task<Stream> DownloadBlob(string containerName, string blobName);
    Task<bool?> DeleteBlob(string containerName, string blobName);
    Task MoveBlob(string containerName, string sourceBlobName, string destinationBlobName, CancellationToken cancellationToken = default);
}

public class BlobStorageRepository : IBlobStorageRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILoggerHelper _loggerHelper;

    public BlobStorageRepository(IConfiguration configuration, ILoggerHelper loggerHelper)
    {
        _configuration = configuration;
        _loggerHelper = loggerHelper;
    }

    public async Task<IEnumerable<BlobItem>> GetAllBlobsFromDirectory(string containerName)
    {
        try
        {
            var container = await GetBlobContainerClient(containerName);
            var result = container.GetBlobs().ToList();

            return result;
        }
        catch (Exception e)
        {
            _loggerHelper.LogError($"Exception when try to get all blobs from container: {containerName}, Message: {e.Message}, StackTrace: {e.StackTrace}");
        }

        return null;
    }

    public async Task<BlobContentInfo> StoreBlob(string containerName, string blobName, Stream content)
    {
        try
        {
            var container = await GetBlobContainerClient(containerName);
            var blob = container.GetBlobClient(blobName);
            var result = await blob.UploadAsync(content, true);

            return result.Value;
        }
        catch (Exception e)
        {
            _loggerHelper.LogError($"Exception when try to blob {blobName} into container: {containerName}, Message: {e.Message}, StackTrace: {e.StackTrace}");
        }

        return null;
    }

    public async Task<Stream> DownloadBlob(string containerName, string blobName)
    {
        try
        {
            var container = await GetBlobContainerClient(containerName);

            var blobClient = container.GetBlobClient(blobName);

            var memoryStream = new MemoryStream();

            await blobClient.DownloadToAsync(memoryStream);

            return memoryStream;
        }
        catch (Exception e)
        {
            _loggerHelper.LogError($"Exception when try to download blob {blobName} from container: {containerName}, Message: {e.Message}, StackTrace: {e.StackTrace}");
        }

        return null;
    }

    public async Task<bool?> DeleteBlob(string containerName, string blobName)
    {
        try
        {
            var container = await GetBlobContainerClient(containerName);
            var response = await container.DeleteBlobIfExistsAsync(blobName);

            return response?.Value;
        }
        catch (Exception e)
        {
            _loggerHelper.LogError($"Exception when try to delete blob {blobName} from container: {containerName}, Message: {e.Message}, StackTrace: {e.StackTrace}");
        }

        return null;
    }

    public async Task MoveBlob(string containerName, string sourceBlobName, string destinationBlobName, CancellationToken cancellationToken = default)
    {
        var containerClient = await GetBlobContainerClient(containerName, cancellationToken);
        var sourceClient = containerClient.GetBlobClient(sourceBlobName);
        var destinationClient = containerClient.GetBlobClient(destinationBlobName);
        var copyOperation = await destinationClient.StartCopyFromUriAsync(sourceClient.Uri, options: new BlobCopyFromUriOptions()
        {
            DestinationConditions = new BlobRequestConditions()
        }
        , cancellationToken);

        await copyOperation.WaitForCompletionAsync(cancellationToken);
        await sourceClient.DeleteAsync();
    }

    private async Task<BlobContainerClient> GetBlobContainerClient(string containerName, CancellationToken cancellationToken = default)
    {
        string connectionString = _configuration["AZURE_STORAGE_CONNECTION_STRING"];

        BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

        if (!await container.ExistsAsync(cancellationToken))
        {
            await container.CreateAsync(cancellationToken: cancellationToken);
        }

        return container;
    }
}
