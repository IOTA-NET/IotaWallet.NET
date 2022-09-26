using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.RequestFromFaucet
{

    public class RequestFromFaucetCommand : IRequest
    {
        public RequestFromFaucetCommand(string address, string url = @"https://faucet.testnet.shimmer.network")
        {
            Address = address;
            Url = url;
        }

        public string Address { get; }
        public string Url { get; }
    }
}
