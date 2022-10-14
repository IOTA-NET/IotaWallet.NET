using IotaWalletNet.Domain.Common.Models.Output;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Queries.GetUnspentOutputs
{
    public class GetUnspentOutputsResponse : RustBridgeResponseBase<List<OutputData>>
    {

    }
}
