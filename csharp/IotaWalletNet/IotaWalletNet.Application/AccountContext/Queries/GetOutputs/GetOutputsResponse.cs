using IotaWalletNet.Domain.Common.Models.Output;
using IotaWalletNet.Domain.PlatformInvoke;

namespace IotaWalletNet.Application.AccountContext.Queries.GetOutputs
{
    public class GetOutputsResponse : RustBridgeResponseBase<List<OutputData>>
    {

    }
}
