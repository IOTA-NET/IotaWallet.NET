using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.DestroyFoundry
{
    public class DestroyFoundryCommandMessage : AccountMessage<DestroyFoundryCommandMessageData>
    {
        private const string METHOD_NAME = "destroyFoundry";

        public DestroyFoundryCommandMessage(string username, DestroyFoundryCommandMessageData messageData)
            : base(username, METHOD_NAME, messageData)
        {

        }
    }
}
