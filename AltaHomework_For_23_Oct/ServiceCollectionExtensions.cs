using System.Reflection;
using AltaHomework_For_23_Oct.Common.Mappings;

namespace AltaHomework_For_23_Oct;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMappingProfiles(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        });

        return serviceCollection;
    }
}
