using FluentValidation;
using Investry.Application.Behaviors.Caching;
using Investry.Application.Common.Mappings;
using Investry.Application.Features.Projects.Commands.CreateProject;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Investry.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(CreateProjectCommand).Assembly);

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }
    }
}
