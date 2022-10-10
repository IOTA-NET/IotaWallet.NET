using IotaWalletNet.Application.Common.HttpClients;
using IotaWalletNet.Application.Common.Interfaces;
using MediatR;
using Polly;
using Polly.Retry;
using Refit;

namespace IotaWalletNet.Application.AccountContext.Commands.RequestFromFaucet
{
    public class RequestFromFaucetCommandHandler : IRequestHandler<RequestFromFaucetCommand>
    {
        private readonly Func<string, IFaucetApi> _faucetApiProvider;
        private readonly AsyncRetryPolicy _retryPolicy;
        public RequestFromFaucetCommandHandler(Func<string, IFaucetApi> faucetApiProvider)
        {
            _faucetApiProvider = faucetApiProvider;
            _retryPolicy = Policy.Handle<ApiException>().WaitAndRetryAsync(retryCount: 3, attemptNumber => TimeSpan.FromSeconds(3));

        }

        public async Task<Unit> Handle(RequestFromFaucetCommand request, CancellationToken cancellationToken)
        {
            IFaucetApi faucetApi = _faucetApiProvider(request.Url);
            Func<Task> faucetRequestFunc = async () => await faucetApi.RequestFromFaucet(new RequestFromFaucetModel(request.Address));

            await _retryPolicy.ExecuteAsync(faucetRequestFunc);

            return Unit.Value;
        }
    }
}
