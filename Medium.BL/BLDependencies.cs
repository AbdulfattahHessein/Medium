using Medium.BL.AppServices;
using Medium.BL.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Medium.BL
{
    public static class BLDependencies
    {

        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services)
        {
            services.AddScoped<IStoriesService, StoriesService>();
            services.AddScoped<IPublishersService, PublishersService>();
            services.AddScoped<IReactionsService, ReactionsService>();
            services.AddScoped<ITopicsService, TopicsService>();
            services.AddScoped<ISavingListServices, SavingListServices>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}
