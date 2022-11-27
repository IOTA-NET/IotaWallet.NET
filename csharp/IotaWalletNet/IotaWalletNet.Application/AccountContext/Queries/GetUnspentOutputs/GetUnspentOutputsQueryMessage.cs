using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetUnspentOutputs
{
    public class GetUnspentOutputsQueryMessage : AccountMessage<GetUnspentOutputsQueryMessageData>
    {
        private const string METHOD_NAME = "unspentOutputs";

        public GetUnspentOutputsQueryMessage(string username, GetUnspentOutputsQueryMessageData messageData)
            : base(username, METHOD_NAME, messageData)
        {

        }
    }
}
