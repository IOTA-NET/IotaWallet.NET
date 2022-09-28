namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class AddressGenerationOptions
    {
        public bool Internal { get; set; } = false;

        public GenerateAddressMetadata Metadata { get; set; }

    }
}
