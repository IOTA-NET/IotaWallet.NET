using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetMinimumStorageDepositRequired
{
    public class GetMinimumStorageDepositRequiredQuery : IRequest<GetMinimumStorageDepositRequiredResponse>
    {
        public IAccount Account { get; set; }

        public string Username { get; set; }
        public IOutputType OutputType { get; }

        public GetMinimumStorageDepositRequiredQuery(string username, IAccount account, IOutputType outputType)
        {
            Account = account;
            Username = username;
            OutputType = outputType;
        }
    }
}
