using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetAddresses
{
    public class GetAddressesQuery : IRequest<GetAddressesResponse>
    {
        public GetAddressesQuery(string username, IAccount account)
        {
            Username = username;
            Account = account;
        }
        public string Username { get; set; }

        public IAccount Account{ get; set; }
    }
}
