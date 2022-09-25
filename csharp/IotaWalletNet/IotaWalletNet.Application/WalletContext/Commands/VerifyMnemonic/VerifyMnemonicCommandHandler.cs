using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic
{
    public class VerifyMnemonicCommandHandler : IRequestHandler<VerifyMnemonicCommand, string>
    {
        public async Task<string> Handle(VerifyMnemonicCommand request, CancellationToken cancellationToken)
        {
            VerifyMnemonicCommandMessage message = new VerifyMnemonicCommandMessage(request.Mnemonic);
            string json = JsonConvert.SerializeObject(message);
            string response = await request.Wallet.SendMessageAsync(json);
            return response;
        }
    }
}
