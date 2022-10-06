using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Nft;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.MintNfts
{
    public class MintNftsCommand : IRequest<MintNftsResponse>
    {
        public MintNftsCommand(IAccount account, string username, List<NftOptions> nftsOptions)
        {
            Account = account;
            Username = username;
            NftsOptions = nftsOptions;
        }

        public IAccount Account { get; }
        public string Username { get; }
        public List<NftOptions> NftsOptions { get; }
    }
}
