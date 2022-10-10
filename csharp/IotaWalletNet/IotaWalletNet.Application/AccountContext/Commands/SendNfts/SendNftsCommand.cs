using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SendNfts
{
    public class SendNftsCommand : IRequest<SendNftsResponse>
    {
        public SendNftsCommand(string username, IAccount account, List<AddressAndNftId> addressAndNftIds)
        {
            Username = username;
            Account = account;
            AddressesAndNftIds = addressAndNftIds;
        }
        public string Username { get; set; }

        public IAccount Account { get; set; }

        public List<AddressAndNftId> AddressesAndNftIds { get; set; }

    }
}
