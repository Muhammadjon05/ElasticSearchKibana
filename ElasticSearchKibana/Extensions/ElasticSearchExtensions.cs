using ElasticSearchKibana.Entities;
using Nest;

namespace ElasticSearchKibana.Extensions;

public static class ElasticSearchExtensions
{
    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration["ELKConfiguration:Url"];
        var defaultIndex = configuration["ELKConfiguration:index"];

        var settings = new ConnectionSettings(new Uri(url!))
            .PrettyJson().DefaultIndex(defaultIndex);
        AddDefaultMapping(settings: settings);
        var client = new ElasticClient(settings);
        services.AddSingleton<IElasticClient>(client);
        

    }

    private static void AddDefaultMapping(ConnectionSettings settings)
    {
        settings.DefaultMappingFor<Product>(
            p => p
                .Ignore(p => p.Id));
    }
    
}   