using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Output;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetOutputsWithAdditionalUnlockConditions
{
    public class GetOutputsWithAdditionalUnlockConditionsQuery : IRequest<GetOutputsWithAdditionalUnlockConditionsResponse>
    {
        private OutputTypeToClaim _outputsTypeToClaim;

        public GetOutputsWithAdditionalUnlockConditionsQuery(OutputTypeToClaim outputsTypeToClaim, string username, IAccount account)
        {
            OutputsTypeToClaim = outputsTypeToClaim;
            Username = username;
            Account = account;
        }

        public OutputTypeToClaim OutputsTypeToClaim { get => _outputsTypeToClaim; set => _outputsTypeToClaim = value; }

        public string Username { get; set; }

        public IAccount Account { get; set; }
    }
}
