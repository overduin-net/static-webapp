public class ClientPrincipal
{
    public string IdentityProvider { get; set; }
    public string UserId { get; set; }
    public string UserDetails { get; set; }
    public IEnumerable<string> UserRoles { get; set; }
}

public interface IAdUserHelper
{
    ClientPrincipal GetStaticWebAppClientPrincipal(HttpRequest req);
}

public class AdUserHelper : IAdUserHelper
{
    public AdUserHelper() { }

    public ClientPrincipal GetStaticWebAppClientPrincipal(HttpRequest req)
    {
        var principal = new ClientPrincipal();

        if (req.Headers.TryGetValue("x-ms-client-principal", out var header))
        {
            var data = header[0];
            var decoded = Convert.FromBase64String(data);
            var json = Encoding.UTF8.GetString(decoded);
            principal = System.Text.Json.JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        principal.UserRoles = principal.UserRoles?.Except(new string[] { Constants.StaticWebAppRole.Anonymous }, StringComparer.CurrentCultureIgnoreCase);

        if (!principal.UserRoles?.Any() ?? true)
        {
            return new ClientPrincipal();
        }

        return principal;
    }
}


