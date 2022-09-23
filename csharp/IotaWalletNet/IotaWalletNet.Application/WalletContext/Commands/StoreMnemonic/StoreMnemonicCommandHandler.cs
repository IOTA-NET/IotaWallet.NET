using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic
{
    public class StoreMnemonicCommandHandler : IRequestHandler<StoreMnemonicCommand>
    {
        public Task<Unit> Handle(StoreMnemonicCommand request, CancellationToken cancellationToken)
        {
            StoreMnemonicCommandMessage message = new StoreMnemonicCommandMessage(request.Mnemonic);
            string messageJson = JsonConvert.SerializeObject(message);
            request.Wallet.SendMessage(messageJson);

            return Unit.Task;
        }
    }
}
