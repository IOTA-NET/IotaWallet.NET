namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class AddressWithAmount
    {
        public AddressWithAmount(string address, string amount)
        {
            Address = address;
            Amount = amount;
        }

        public string Address { get; }
        public string Amount { get; }
    }
}
