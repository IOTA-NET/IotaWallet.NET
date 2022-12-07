using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetFoundryOutput
{
    public class GetFoundryOutputQueryMessage : AccountMessage<GetFoundryOutputQueryMessageData>
    {
        private const string METHOD_NAME = "getFoundryOutput";
        public GetFoundryOutputQueryMessage(string username, GetFoundryOutputQueryMessageData messageData)
            :base(username, METHOD_NAME, messageData)
        {

        }
    }
}
