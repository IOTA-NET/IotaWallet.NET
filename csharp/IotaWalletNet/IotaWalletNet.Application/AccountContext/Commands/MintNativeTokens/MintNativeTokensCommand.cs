using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.MintNativeTokens
{
    public class MintNativeTokensCommand : IRequest<MintNativeTokensResponse>
    {
        public MintNativeTokensCommand(string username, IAccount account, NativeTokenOptions nativeTokenOptions)
        {
            Username = username;
            Account = account;
            NativeTokenOptions = nativeTokenOptions;
        }

        public string Username { get; set; }
        public IAccount Account { get; set; }

        public NativeTokenOptions NativeTokenOptions { get; set; }
    }

}
