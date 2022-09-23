using IotaWalletNet.Domain.Common.Notifications;
using MediatR;

namespace IotaWalletNet.Testbed.Common.Notifications
{
    public class MessageReceivedNotificationHandler : INotificationHandler<MessageReceivedNotification>
    {
        public Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(notification.Message))
                Console.WriteLine($"Message: {notification.Message}");

            if (!string.IsNullOrEmpty(notification.Error))
                Console.WriteLine($"Rust Bridge Errors: {notification.Error}");

            return Task.CompletedTask;
        }
    }
}
