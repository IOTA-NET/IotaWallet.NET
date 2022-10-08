using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetAddresses
{
    public class GetAddressesQueryMessage : AccountMessage
    {
        private const string METHOD_NAME = "addresses";
        public GetAddressesQueryMessage(string username)
            : base(username, METHOD_NAME)
        {

        }
    }
}
