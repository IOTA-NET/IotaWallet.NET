using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic
{
    public class GetNewMnemonicQueryHandler : IRequestHandler<GetNewMnemonicQuery, string>
    {
        public async Task<string> Handle(GetNewMnemonicQuery request, CancellationToken cancellationToken)
        {
            GetNewMnemonicQueryMessage message = new GetNewMnemonicQueryMessage();
            string json = JsonConvert.SerializeObject(message);
            string response = await request.Wallet.SendMessageAsync(json);

            return response;
        }
    }
}
