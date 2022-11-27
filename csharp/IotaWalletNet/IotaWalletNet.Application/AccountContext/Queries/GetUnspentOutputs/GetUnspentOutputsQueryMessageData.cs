using IotaWalletNet.Domain.Common.Models.Output;

namespace IotaWalletNet.Application.AccountContext.Queries.GetUnspentOutputs
{
    public class GetUnspentOutputsQueryMessageData
    {
        public GetUnspentOutputsQueryMessageData(OutputFilterOptions? filterOptions=null)
        {
            FilterOptions = filterOptions;
        }
        public OutputFilterOptions? FilterOptions { get; set; }
    }
}
