using Microsoft.Extensions.DependencyInjection;

namespace ToDo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services;
        }



    }
}
