using PUB.Domain.Notifications;

namespace PUB.Domain.Interfaces
{
    public interface INotificator
    {
        void Handle(Notification notificacao);

        List<Notification> GetNotifications();

        bool HasNotification();
    }
}