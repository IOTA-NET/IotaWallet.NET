using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.RequestFromFaucet
{

    public class RequestFromFaucetCommand : IRequest
    {
        public RequestFromFaucetCommand(string address, string url)
        {
            Address = address;
            Url = url;
        }

        public string Address { get; }
        public string Url { get; }
    }
}
