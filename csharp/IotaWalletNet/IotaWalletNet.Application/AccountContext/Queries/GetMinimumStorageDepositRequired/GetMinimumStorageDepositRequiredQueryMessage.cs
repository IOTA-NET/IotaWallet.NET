using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetMinimumStorageDepositRequired
{
    public class GetMinimumStorageDepositRequiredQueryMessage : AccountMessage<IOutputType>
    {
        private const string METHOD_NAME = "minimumRequiredStorageDeposit";
        public GetMinimumStorageDepositRequiredQueryMessage(string username, IOutputType outputType)
            : base(username, METHOD_NAME, outputType)
        {

        }
    }
}
