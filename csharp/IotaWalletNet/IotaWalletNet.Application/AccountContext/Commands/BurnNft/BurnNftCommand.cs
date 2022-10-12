using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.BurnNft
{
    public class BurnNftCommand : IRequest<BurnNftResponse>
    {
        public BurnNftCommand(string nftId, string username, IAccount account)
        {
            NftId = nftId;
            Username = username;
            Account = account;
        }

        public string NftId { get; set; }
        public string Username { get; set; }

        public IAccount Account { get; set; }
    }
}
