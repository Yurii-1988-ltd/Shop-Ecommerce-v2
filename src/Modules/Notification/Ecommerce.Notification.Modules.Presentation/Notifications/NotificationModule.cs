

using Ecommerce.Presentation;
using Ecommerce.Notification.Modules.Infrastructure;


namespace Ecommerce.Notification.Modules.Presentation.Notifications
{
    public class NotificationModule :IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            // Когда добавите HTTP-эндпоинты (например, получение истории уведомлений), смаппите их тут:
             new GetNotificationsEndpoint().MapEndpoint(app);
        }

        public void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            // Делегируем регистрацию сервисов в Infrastructure
            services.AddNotificationsModule(config);
        }
    }
}
