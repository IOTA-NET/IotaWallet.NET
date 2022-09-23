using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic
{
    public class VerifyMnemonicCommandHandler : IRequestHandler<VerifyMnemonicCommand>
    {
        public Task<Unit> Handle(VerifyMnemonicCommand request, CancellationToken cancellationToken)
        {
            VerifyMnemonicCommandMessage message = new VerifyMnemonicCommandMessage(request.Mnemonic);
            string json = JsonConvert.SerializeObject(message);
            request.Wallet.SendMessage(json);

            return Unit.Task;
        }
    }
}
