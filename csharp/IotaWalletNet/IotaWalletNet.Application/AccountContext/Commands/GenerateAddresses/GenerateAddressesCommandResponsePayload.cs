namespace IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses
{
    public class GenerateAddressesCommandResponsePayload
    {
        public string? Address { get; set; }
        public uint KeyIndex { get; set; }
        public bool Internal { get; set; }

        public bool Used { get; set; }
    }
}
