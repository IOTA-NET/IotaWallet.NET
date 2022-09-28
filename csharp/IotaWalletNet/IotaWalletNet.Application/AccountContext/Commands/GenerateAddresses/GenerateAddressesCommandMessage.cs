using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses
{
    public class GenerateAddressesCommandMessage : AccountMessage<GenerateAddressesData>
    {
        private const string METHOD_NAME = "GenerateAddresses";
        public GenerateAddressesCommandMessage(string username, GenerateAddressesData methodData)
            : base(username, METHOD_NAME, methodData)
        {

        }
    }
}
