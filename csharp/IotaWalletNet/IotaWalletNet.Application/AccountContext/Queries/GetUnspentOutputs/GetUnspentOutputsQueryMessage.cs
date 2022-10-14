using IotaWalletNet.Domain.Common.Models;
using IotaWalletNet.Domain.Common.Models.Output;

namespace IotaWalletNet.Application.AccountContext.Queries.GetUnspentOutputs
{
    public class GetUnspentOutputsQueryMessage : AccountMessage<OutputFilterOptions>
    {
        private const string METHOD_NAME = "unspentOutputs";

        public GetUnspentOutputsQueryMessage(string username, OutputFilterOptions? outputFilterOptions)
            : base(username, METHOD_NAME, outputFilterOptions)
        {

        }
    }
}
