using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic
{
    public class VerifyMnemonicCommandHandler : IRequestHandler<VerifyMnemonicCommand, VerifyMnemonicResponse>
    {
        public async Task<VerifyMnemonicResponse> Handle(VerifyMnemonicCommand request, CancellationToken cancellationToken)
        {
            VerifyMnemonicCommandMessage message = new VerifyMnemonicCommandMessage(request.Mnemonic);
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Wallet.SendMessageAsync(json);

            VerifyMnemonicResponse response = genericResponse.As<VerifyMnemonicResponse>()!;
            return response;
        }
    }
}
