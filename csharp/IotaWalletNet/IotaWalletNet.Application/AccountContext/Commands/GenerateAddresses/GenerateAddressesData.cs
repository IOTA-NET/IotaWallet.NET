using IotaWalletNet.Domain.Common.Models.Address;

namespace IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses
{
    public class GenerateAddressesData
    {
        public GenerateAddressesData(uint amount, AddressGenerationOptions addressGenerationOptions)
        {
            Amount = amount;
            AddressGenerationOptions = addressGenerationOptions;
        }

        public uint Amount { get; }
        public AddressGenerationOptions AddressGenerationOptions { get; }
    }
}
