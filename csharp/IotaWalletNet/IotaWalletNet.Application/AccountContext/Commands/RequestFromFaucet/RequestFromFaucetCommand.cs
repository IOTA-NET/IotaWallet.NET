using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.RequestFromFaucet
{
    public class RequestFromFaucetCommand : IRequest<string>
    {
        public RequestFromFaucetCommand(IAccount account, string username, string url = @"https://faucet.testnet.shimmer.network/api/enqueue")
        {
            Account = account;
            Username = username;
            Url = url;
        }
        
        
        public IAccount Account { get; }
        public string Username { get; }
        public string Url { get; }
    }
}
