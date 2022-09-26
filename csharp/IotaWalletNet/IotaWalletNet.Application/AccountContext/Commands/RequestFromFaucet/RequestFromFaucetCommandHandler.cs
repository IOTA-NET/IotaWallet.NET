using IotaWalletNet.Application.Common.HttpClients;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.RequestFromFaucet
{
    public class RequestFromFaucetCommandHandler : IRequestHandler<RequestFromFaucetCommand>
    {
        private readonly Func<string, IFaucetApi> _faucetApiProvider;

        public RequestFromFaucetCommandHandler(Func<string, IFaucetApi> faucetApiProvider)
        {
            _faucetApiProvider = faucetApiProvider;
        }

        public async Task<Unit> Handle(RequestFromFaucetCommand request, CancellationToken cancellationToken)
        {
            IFaucetApi faucetApi = _faucetApiProvider(request.Url);
            await faucetApi.RequestFromFaucet(new RequestFromFaucetModel(request.Address));
            return Unit.Value;
        }
    }
}
