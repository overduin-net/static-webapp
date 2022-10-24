[assembly: FunctionsStartup(typeof(Startup))]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.GetContext().Configuration;

        builder.Services.AddHttpContextAccessor();

        // Setup services DI.
        //builder.Services.AddScoped<ISessionService, SessionService>();
        //builder.Services.AddScoped<IPlayerService, PlayerService>();

        // Add the Graph client service
        //builder.Services.AddSingleton<IGraphClientService, GraphClientService>();

        // Setup repositories DI
        builder.Services.AddScoped<ITableStorageRepository, TableStorageRepository>();
        builder.Services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();



        // Setup helper DI
        builder.Services.AddScoped<ILoggerHelper, LoggerHelper>();
        //builder.Services.AddScoped<IAdUserHelper, AdUserHelper>();

        // Add 3rd party toolings.
        AddSwashBuckle(builder); // add open-api swagger documentation.

        // Auto Mapper Configurations
        builder.Services.AddAutoMapper(Assembly.GetAssembly(this.GetType()));
    }

    private void AddSwashBuckle(IFunctionsHostBuilder builder)
    {
        builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts =>
        {
            opts.SpecVersion = OpenApiSpecVersion.OpenApi3_0;
            opts.AddCodeParameter = true;
            opts.PrependOperationWithRoutePrefix = true;
            opts.Documents = new[]
            {
                    new SwaggerDocument
                    {
                        Name = "v1",
                        Title = "Swagger document",
                        Description = "Swagger v1 document",
                        Version = "v1"
                    }
            };
            opts.Title = "Swagger OpenAPI";
            opts.ConfigureSwaggerGen = x =>
            {
                x.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
                    ? methodInfo.Name
                    : new Guid().ToString());
            };
        });
    }
}