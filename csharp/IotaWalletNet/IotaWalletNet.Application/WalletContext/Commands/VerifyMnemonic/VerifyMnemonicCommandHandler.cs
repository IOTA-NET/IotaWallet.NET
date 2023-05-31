using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic
{
    public class VerifyMnemonicCommandHandler : IRequestHandler<VerifyMnemonicCommand, VerifyMnemonicResponse>
    {
        public async Task<VerifyMnemonicResponse> Handle(VerifyMnemonicCommand request, CancellationToken cancellationToken)
        {
            VerifyMnemonicCommandMessageData messageData = new VerifyMnemonicCommandMessageData(request.Mnemonic);
            VerifyMnemonicCommandMessage message = new VerifyMnemonicCommandMessage(messageData);
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Wallet.SendMessageAsync(json);

            VerifyMnemonicResponse response = genericResponse.As<VerifyMnemonicResponse>()!;
            return response;
        }
    }
}
