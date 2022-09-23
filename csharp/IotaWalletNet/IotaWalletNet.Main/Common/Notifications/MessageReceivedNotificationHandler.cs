using IotaWalletNet.Domain.Common.Notifications;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Main.Common.Notifications
{
    public class MessageReceivedNotificationHandler : INotificationHandler<MessageReceivedNotification>
    {
        public Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(notification.Message))
                Console.WriteLine($"Message: {PrettyJson(notification.Message)}");

            if (!string.IsNullOrEmpty(notification.Error))
                Console.WriteLine($"Rust Bridge Errors: {PrettyJson(notification.Error)}");

            return Task.CompletedTask;
        }

        private string PrettyJson(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json)!;
            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }
    }
}
