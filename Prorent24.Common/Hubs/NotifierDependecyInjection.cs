using Microsoft.Extensions.DependencyInjection;

namespace Prorent24.Common.Hubs
{
    public static class NotifierDependecyInjection
    {
        public static void AddNotifier(this IServiceCollection services)
        {
            services.AddTransient<INotifier, Notifier>();
        }
    }
}
