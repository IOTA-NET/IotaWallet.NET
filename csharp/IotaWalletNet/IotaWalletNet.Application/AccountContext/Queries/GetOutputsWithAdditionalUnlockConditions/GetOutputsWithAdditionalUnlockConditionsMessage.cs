using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetOutputsWithAdditionalUnlockConditions
{
    public class GetOutputsWithAdditionalUnlockConditionsMessage : AccountMessage<GetOutputsWithAdditionalUnlockConditionsMessageData>
    {
        private const string METHOD_NAME = "getOutputsWithAdditionalUnlockConditions";

        public GetOutputsWithAdditionalUnlockConditionsMessage(string username, GetOutputsWithAdditionalUnlockConditionsMessageData? methodData) 
            : base(username, METHOD_NAME, methodData)
        {
        }
    }
}
