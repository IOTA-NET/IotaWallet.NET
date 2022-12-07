using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetFoundryOutput
{
    public class GetFoundryOutputQuery : IRequest<GetFoundryOutputResponse>
    {
        public GetFoundryOutputQuery(string tokenId, string username, IAccount account)
        {
            TokenId = tokenId;
            Username = username;
            Account = account;
        }

        public string TokenId { get; set; }

        public string Username { get; set; }

        public IAccount Account { get; set; }
    }
}
