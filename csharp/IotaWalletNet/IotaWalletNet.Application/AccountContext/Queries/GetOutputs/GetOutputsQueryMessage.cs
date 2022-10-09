using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetOutputs
{
    public class GetOutputsQueryMessage : AccountMessage<GetOutputsQueryMessageData>
    {
        private const string METHOD_NAME = "outputs";

        public GetOutputsQueryMessage(string username, GetOutputsQueryMessageData getOutputsQueryMessageData)
            :base(username, METHOD_NAME, getOutputsQueryMessageData)
        {

        }
    }
}
