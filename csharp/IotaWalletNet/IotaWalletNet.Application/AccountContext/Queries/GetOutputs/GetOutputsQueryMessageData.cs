using IotaWalletNet.Domain.Common.Models.Output;

namespace IotaWalletNet.Application.AccountContext.Queries.GetOutputs
{
    public class GetOutputsQueryMessageData
    {
        public OutputFilterOptions? FilterOptions { get; set; }

        public GetOutputsQueryMessageData(OutputFilterOptions? filterOptions)
        {
            FilterOptions = filterOptions;
        }
    }
}
