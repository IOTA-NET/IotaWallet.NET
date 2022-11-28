using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommandMessage : AccountMessage<SendAmountCommandMessageData>
    {

        private const string METHOD_NAME = "sendAmount";

        public SendAmountCommandMessage(string username, SendAmountCommandMessageData methodData)
            : base(username, METHOD_NAME, methodData)
        {
        }
    }
}
