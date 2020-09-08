using Distribuicao.Lucros.Domain.Interfaces.Services;
using Distribuicao.Lucros.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Distribuicao.Lucros.Infra.Ioc.DependencyInjections
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                //.ConfigureRepositories(connectionString)
                .ConfigureDomainServices();
        }

        private static IServiceCollection ConfigureDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IDistribuicaoService, DistribuicaoService>();
            return services;
        }
    }
}
