using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic
{
    public class StoreMnemonicCommandHandler : IRequestHandler<StoreMnemonicCommand, StoreMnemonicResponse>
    {
        public async Task<StoreMnemonicResponse> Handle(StoreMnemonicCommand request, CancellationToken cancellationToken)
        {
            StoreMnemonicCommandMessage message = new StoreMnemonicCommandMessage(request.Mnemonic);
            string messageJson = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Wallet.SendMessageAsync(messageJson);


            StoreMnemonicResponse response = genericResponse.As<StoreMnemonicResponse>()!;

            return response;
        }
    }
}
