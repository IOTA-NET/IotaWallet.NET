using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.SendNfts
{
    public class SendNftsCommandMessageData
    {
        public SendNftsCommandMessageData(List<AddressAndNftId> addressesAndNftIds)
        {
            AddressesAndNftIds = addressesAndNftIds;
        }

        public List<AddressAndNftId> AddressesAndNftIds { get; set; }

        public TransactionOptions Options { get; set; } = new TransactionOptions();
    }
}
