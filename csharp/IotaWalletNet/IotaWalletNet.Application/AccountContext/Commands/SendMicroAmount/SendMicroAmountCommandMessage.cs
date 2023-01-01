using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.SendMicroAmount
{
    public class SendMicroAmountCommandMessage : AccountMessage<SendMicroAmountCommandMessageData>
    {
        private const string METHOD_NAME = "sendMicroTransaction";
        public SendMicroAmountCommandMessage(string username, SendMicroAmountCommandMessageData? methodData)
            : base(username, METHOD_NAME, methodData)
        {
        }
    }
}
