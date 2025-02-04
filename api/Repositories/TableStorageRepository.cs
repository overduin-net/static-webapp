public interface ITableStorageRepository
{
    Task<T> InsertOrMergeEntityAsync<T>(T entity) where T : class, ITableEntity, new();
    Task<IEnumerable<T>> InsertOrMergeEntityAsync<T>(IEnumerable<T> entities) where T : class, ITableEntity, new();
    Task<T> ReplaceEntityAsync<T>(T entity) where T : class, ITableEntity, new();
    Task<IEnumerable<T>> ReplaceEntityAsync<T>(IEnumerable<T> entitities) where T : class, ITableEntity, new();
    Task DeleteEntityAsync<T>(string partitionKey, string rowKey) where T : class, ITableEntity, new();
    Task<T> GetEntityAsync<T>(string rowKey) where T : class, ITableEntity, new();
    Task<IEnumerable<T>> GetEntities<T>(string value, string propertyName = Constants.PartitionKey) where T : class, ITableEntity, new();
    Task<IEnumerable<T>> Search<T>(string filter) where T : class, ITableEntity, new();
    Task<IEnumerable<T>> Filter<T>(Expression<Func<T, bool>> predicate) where T : class, ITableEntity, new();
    Task<IEnumerable<T>> GetAllEntityRows<T>() where T : class, ITableEntity, new();
}

public class TableStorageRepository : ITableStorageRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILoggerHelper _loggerHelper;

    private const string _tablePrefix = "";
    private const string _tableSuffix = "Data";

    public TableStorageRepository(IConfiguration configuration, ILoggerHelper loggerHelper)
    {
        _configuration = configuration;
        _loggerHelper = loggerHelper;
    }

    public async Task<TableClient> CreateTableClientAsync(string tableName)
    {
        string connectionString = _configuration["AZURE_STORAGE_CONNECTION_STRING"];
        TableClient client = new TableClient(connectionString, tableName);
        await client.CreateIfNotExistsAsync();

        return client;
    }

    public async Task<T> InsertOrMergeEntityAsync<T>(T entity) where T : class, ITableEntity, new()
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }

        try
        {
            var table = await GetTableClient<T>();

            await table.UpsertEntityAsync(entity, TableUpdateMode.Merge);
        }
        catch (Exception e)
        {
            _loggerHelper.LogInformation($"Exception when trying to insert or merge entity. Message: {e.Message}. StackTrace: {e.StackTrace}");
        }

        return entity;
    }

    public async Task<IEnumerable<T>> InsertOrMergeEntityAsync<T>(IEnumerable<T> entities) where T : class, ITableEntity, new()
    {
        var table = await GetTableClient<T>();
        foreach (var entity in entities)
        {
            await table.UpsertEntityAsync(entity, TableUpdateMode.Merge);
        }

        return entities;
    }

    public async Task<T> ReplaceEntityAsync<T>(T entity) where T : class, ITableEntity, new()
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }

        try
        {
            var table = await GetTableClient<T>();
            await table.UpsertEntityAsync(entity, TableUpdateMode.Replace);
        }
        catch (Exception e)
        {
            _loggerHelper.LogInformation($"Exception when trying to replace entity. Message: {e.Message}. StackTrace: {e.StackTrace}");
        }

        return entity;
    }

    public async Task<IEnumerable<T>> ReplaceEntityAsync<T>(IEnumerable<T> entities) where T : class, ITableEntity, new()
    {
        var table = await GetTableClient<T>();
        foreach (var entity in entities)
        {
            await table.UpsertEntityAsync(entity, TableUpdateMode.Replace);
        }

        return entities;
    }

    public async Task DeleteEntityAsync<T>(string partitionKey, string rowKey) where T : class, ITableEntity, new()
    {
        try
        {
            var table = await GetTableClient<T>();

            await table.DeleteEntityAsync(partitionKey, rowKey);
        }
        catch (Exception e)
        {
            _loggerHelper.LogInformation($"Exception when trying to delete entity. Message: {e.Message}. StackTrace: {e.StackTrace}");
        }
    }

    public async Task<T> GetEntityAsync<T>(string rowkey) where T : class, ITableEntity, new()
    {
        T result = default(T);
        try
        {
            var results = await GetEntities<T>(rowkey);
            result = results.FirstOrDefault();
        }
        catch (Exception e)
        {
            _loggerHelper.LogInformation(
                $"Exception when trying to get entity async. Message: {e.Message}. StackTrace: {e.StackTrace}");
        }

        return result;
    }

    public async Task<IEnumerable<T>> GetEntities<T>(string value, string propertyName = Constants.RowKey) where T : class, ITableEntity, new()
    {
        IEnumerable<T> results = null;
        try
        {
            var table = await GetTableClient<T>();
            if (propertyName == Constants.PartitionKey)
            {
                results = table.Query<T>(x => x.PartitionKey == value);
            }
            if (propertyName == Constants.RowKey)
            {
                results = table.Query<T>(x => x.RowKey == value);
            }
        }
        catch (Exception e)
        {
            _loggerHelper.LogInformation($"Exception when trying to get entities. Message: {e.Message}. StackTrace: {e.StackTrace}");
        }

        return results;
    }


    public async Task<IEnumerable<T>> Search<T>(string filter) where T : class, ITableEntity, new()
    {
        IEnumerable<T> results = null;
        try
        {
            var table = await GetTableClient<T>();

            // odata filter: "PartitionKey eq 'foo'"
            var entities = table.Query<T>(filter);
            results = entities;
        }
        catch (Exception e)
        {
            _loggerHelper.LogInformation(
                $"Exception when trying to search entities. Message: {e.Message}. StackTrace: {e.StackTrace}");
        }

        return results;
    }

    public async Task<IEnumerable<T>> Filter<T>(Expression<Func<T, bool>> predicate) where T : class, ITableEntity, new()
    {
        IEnumerable<T> results = null;

        try
        {
            var table = await GetTableClient<T>();
            var entities = table.Query<T>(predicate);

            results = entities;
        }
        catch (Exception e)
        {
            _loggerHelper.LogInformation(
                $"Exception when trying to search entities. Message: {e.Message}. StackTrace: {e.StackTrace}");
        }

        return results;
    }

    public async Task<IEnumerable<T>> GetAllEntityRows<T>() where T : class, ITableEntity, new()
    {
        var results = new List<T>();

        try
        {
            var table = await GetTableClient<T>();

            results = table.Query<T>().ToList();
        }
        catch (Exception e)
        {
            _loggerHelper.LogInformation(
                $"Exception when trying to get all the entitie rows from table. Message: {e.Message}. StackTrace: {e.StackTrace}");
            throw;
        }

        return results.OrderByDescending(x => x.Timestamp);
    }

    #region Private Members
    private async Task<TableClient> GetTableClient<T>() where T : ITableEntity
    {
        return await CreateTableClientAsync($"{_tablePrefix}{typeof(T).Name}{_tableSuffix}");
    }
    #endregion
}