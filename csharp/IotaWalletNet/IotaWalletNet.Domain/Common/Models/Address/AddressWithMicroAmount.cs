namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class AddressWithMicroAmount
    {
        public AddressWithMicroAmount(string address, string amount, ulong expiration, string? returnAddress=null)
        {
            Address = address;
            Amount = amount;
            ReturnAddress = returnAddress;
            Expiration = expiration;
        }

        public string Address { get; set; }

        public string Amount { get; set; }

        public string? ReturnAddress { get; set; }

        public ulong Expiration { get; set; }
    }


}
