using PUB.Domain.Interfaces;

namespace PUB.Application.Notifications
{
    public class Notificator : INotificator
    {
        private List<string> _notifications;

        public Notificator()
        {
            _notifications = new List<string>();
        }

        public void Handle(string notificacao)
        {
            _notifications.Add(notificacao);
        }

        public List<string> GetNotifications()
        {
            return _notifications;
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}