namespace PUB.Domain.Interfaces
{
    public interface INotificator
    {
        void Handle(string notificacao);

        List<string> GetNotifications();

        bool HasNotification();
    }
}