using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetAddressesWithUnspentOutputs
{
    public class GetAddressesWithUnspentOutputsQueryMessage : AccountMessage
    {
        private const string METHOD_NAME = "addressesWithUnspentOutputs";
        public GetAddressesWithUnspentOutputsQueryMessage(string username)
            : base(username, METHOD_NAME)
        {

        }
    }
}
