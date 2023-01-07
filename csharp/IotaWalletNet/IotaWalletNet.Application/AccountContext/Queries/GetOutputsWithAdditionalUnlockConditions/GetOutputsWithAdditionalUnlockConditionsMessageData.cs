using IotaWalletNet.Domain.Common.Models.Output;

namespace IotaWalletNet.Application.AccountContext.Queries.GetOutputsWithAdditionalUnlockConditions
{
    public class GetOutputsWithAdditionalUnlockConditionsMessageData
    {
        public GetOutputsWithAdditionalUnlockConditionsMessageData(OutputTypeToClaim outputTypeToClaim)
        {
            OutputsToClaim = outputTypeToClaim.ToString();
        }

        public string OutputsToClaim { get; set; }
    }
}
