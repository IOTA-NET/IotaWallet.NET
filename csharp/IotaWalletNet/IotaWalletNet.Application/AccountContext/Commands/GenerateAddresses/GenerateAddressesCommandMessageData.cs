using IotaWalletNet.Domain.Common.Models.Address;

namespace IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses
{
    public class GenerateAddressesCommandMessageData
    {
        public GenerateAddressesCommandMessageData(uint amount, AddressGenerationOptions addressGenerationOptions)
        {
            Amount = amount;
            AddressGenerationOptions = addressGenerationOptions;
        }

        public uint Amount { get; }
        public AddressGenerationOptions AddressGenerationOptions { get; }
    }
}
