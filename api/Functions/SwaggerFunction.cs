public static class SwaggerFunction
{
    [FunctionName("SwaggerJson")]
    [SwaggerIgnore]
    public static Task<HttpResponseMessage> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "json")]
        HttpRequestMessage req, ILogger log, [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
    {
        return Task.FromResult(swashBuckleClient.CreateSwaggerDocumentResponse(req));
    }

    [FunctionName("SwaggerUI")]
    [SwaggerIgnore]
    public static Task<HttpResponseMessage> RunUI(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")]
        HttpRequestMessage req, ILogger log,
    [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
    {
        return Task.FromResult(swashBuckleClient.CreateSwaggerUIResponse(req, "json"));
    }
}