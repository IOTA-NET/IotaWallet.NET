using MediatR;

namespace IotaWalletNet.Application.Common.Notifications
{
    /// <summary>
    ///In order to subscribe for this notification, inherit from INotificationHandler<CallbackNotificaton> 
    /// </summary>
    /// <see cref="https://github.com/jbogard/MediatR/wiki"/>
    public class MessageReceivedNotification : INotification
    {
        //TODO: implement context as well
        public MessageReceivedNotification(string message, string error)
        {
            Message = message;
            Error = error;
        }

        public string Message { get; }
        public string Error { get; }
    }
}
