using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Application
{
    public class Account : IAccount
    {
        public string Username { get;}

        public Account(string username)
        {
            Username = username;
        }
    }
}
