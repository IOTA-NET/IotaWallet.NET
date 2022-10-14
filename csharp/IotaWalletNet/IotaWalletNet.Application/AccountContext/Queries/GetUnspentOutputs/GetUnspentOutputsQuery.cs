using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetUnspentOutputs
{
    public class GetUnspentOutputsQuery : IRequest<GetUnspentOutputsResponse>
    {
        public string Username { get; set; }

        public IAccount Account { get; set; }

        public GetUnspentOutputsQuery(string username, IAccount account)
        {
            Username = username;
            Account = account;
        }
    }
}
