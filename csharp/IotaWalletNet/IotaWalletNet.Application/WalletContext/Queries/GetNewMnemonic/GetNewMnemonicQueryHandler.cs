using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic
{
    public class GetNewMnemonicQueryHandler : IRequestHandler<GetNewMnemonicQuery>
    {
        public Task<Unit> Handle(GetNewMnemonicQuery request, CancellationToken cancellationToken)
        {
            GetNewMnemonicQueryMessage message = new GetNewMnemonicQueryMessage();
            string json = JsonConvert.SerializeObject(message);
            request.Wallet.SendMessage(json);

            return Unit.Task;
        }
    }
}
