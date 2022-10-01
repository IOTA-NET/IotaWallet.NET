namespace IotaWalletNet.Application.Common.HttpClients
{
    public record RequestFromFaucetModel
    {
        public RequestFromFaucetModel(string address)
        {
            Address = address;
        }
        public string Address { get; set; }
    }


}
