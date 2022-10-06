using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses
{
    public class GenerateAddressesCommandMessage : AccountMessage<GenerateAddressesCommandMessageData>
    {
        private const string METHOD_NAME = "generateAddresses";
        public GenerateAddressesCommandMessage(string username, GenerateAddressesCommandMessageData methodData)
            : base(username, METHOD_NAME, methodData)
        {

        }
    }
}
