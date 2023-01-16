using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.SendOutputs
{
    public class SendOutputsCommandMessage : AccountMessage<SendOutputsCommandMessageData>
    {
        private const string METHOD_NAME = "sendOutputs";
        public SendOutputsCommandMessage(string username, SendOutputsCommandMessageData? methodData) 
            : base(username, METHOD_NAME, methodData)
        {
        }
    }
}
