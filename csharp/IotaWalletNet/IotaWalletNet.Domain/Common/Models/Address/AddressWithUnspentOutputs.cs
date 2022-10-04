namespace IotaWalletNet.Domain.Common.Models.Address
{
    public class AddressWithUnspentOutputs
    {
        public AddressWithUnspentOutputs(string address)
        {
            Address = address;
        }

        public string Address { get; set; }

        public int KeyIndex { get; set; }

        public bool Internal { get; set; }

        public List<string> OutputIds { get; set; } = new List<string>();
    }
}
