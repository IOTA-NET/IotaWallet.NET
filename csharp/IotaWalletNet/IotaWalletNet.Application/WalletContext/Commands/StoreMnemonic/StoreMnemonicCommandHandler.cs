using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic
{
    public class StoreMnemonicCommandHandler : IRequestHandler<StoreMnemonicCommand, string>
    {
        public async Task<string> Handle(StoreMnemonicCommand request, CancellationToken cancellationToken)
        {
            StoreMnemonicCommandMessage message = new StoreMnemonicCommandMessage(request.Mnemonic);
            string messageJson = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse response = await request.Wallet.SendMessageAsync(messageJson);

            return "";
        }
    }
}
