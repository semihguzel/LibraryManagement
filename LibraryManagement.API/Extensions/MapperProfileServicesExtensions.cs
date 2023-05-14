using LibraryManagement.Service.Book.Mappings;

namespace LibraryManagement.API.Extensions;

public static class MapperProfileServicesExtensions
{
    public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BookProfiles));

        return services;
    }
}