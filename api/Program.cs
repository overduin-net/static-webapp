var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder =>
    {

    })
    .ConfigureAppConfiguration((hostContext, services) =>
    {

    })
    .ConfigureServices((hostContext, services) =>
    {
      services.AddHttpContextAccessor();
      services.AddScoped<JwtSecurityTokenHandler>();
      services.AddScoped<ITableStorageRepository, TableStorageRepository>();
      services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();
      services.AddScoped<ILoggerHelper, LoggerHelper>();
      services.AddScoped<IAdUserHelper, AdUserHelper>();
      services.AddAutoMapper(typeof(Program).Assembly);

      services.AddSwashBuckle(opts =>
      {
        opts.RoutePrefix = "api";
        opts.SpecVersion = OpenApiSpecVersion.OpenApi3_0;
        opts.AddCodeParameter = true;
        opts.PrependOperationWithRoutePrefix = true;
        opts.Documents = new[]
        {
                new SwaggerDocument
                {
                    Name = "v1",
                    Title = "T Shape Analyzer",
                    Description = "Employee Competencies",
                    Version = "v1"
                }
            };
        opts.Title = "Static WebApp Material";
        opts.ConfigureSwaggerGen = x =>
        {
          x.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
              ? methodInfo.Name
              : new Guid().ToString());
        };
      });
    })
    .Build();

host.Run();
