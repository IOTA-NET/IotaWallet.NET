using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Address;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SendNativeTokens
{
    public class SendNativeTokensCommand : IRequest<SendNativeTokensResponse>
    {

        public SendNativeTokensCommand(string username, IAccount account, List<AddressWithNativeTokens> addressWithNativeTokens)
        {
            Username = username;
            Account = account;
            AddressWithNativeTokens = addressWithNativeTokens;
        }
        public string Username { get; set; }

        public IAccount Account { get; set; }

        public List<AddressWithNativeTokens> AddressWithNativeTokens { get; set; }


    }
}
