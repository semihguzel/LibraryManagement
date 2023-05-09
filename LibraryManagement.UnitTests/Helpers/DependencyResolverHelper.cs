using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryManagement.UnitTests.Helpers;

public class DependencyResolverHelper
{
    private readonly IHost _host;

    public DependencyResolverHelper(IHost host) => _host = host;

    public T GetService<T>() where T : notnull
    {
        var serviceScope = _host.Services.CreateScope();
        var services = serviceScope.ServiceProvider;

        try
        {
            return services.GetRequiredService<T>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}