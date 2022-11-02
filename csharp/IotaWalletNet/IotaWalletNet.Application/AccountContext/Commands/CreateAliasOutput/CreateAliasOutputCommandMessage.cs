using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.CreateAliasOutput
{
    public class CreateAliasOutputCommandMessage : AccountMessage<CreateAliasOutputCommandMessageData>
    {
        private const string METHOD_NAME = "createAliasOutput";
        public CreateAliasOutputCommandMessage(string username, CreateAliasOutputCommandMessageData messageData)
            : base(username, METHOD_NAME, messageData)
        {

        }
    }
}
