using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic
{
    public class VerifyMnemonicCommandHandler : IRequestHandler<VerifyMnemonicCommand, VerifyMnemonicCommandResponse>
    {
        public async Task<VerifyMnemonicCommandResponse> Handle(VerifyMnemonicCommand request, CancellationToken cancellationToken)
        {
            VerifyMnemonicCommandMessage message = new VerifyMnemonicCommandMessage(request.Mnemonic);
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Wallet.SendMessageAsync(json);

            VerifyMnemonicCommandResponse response = genericResponse.IsSuccess
                                                                    ? genericResponse.As<VerifyMnemonicCommandResponse>()!
                                                                    : new VerifyMnemonicCommandResponse() { Error = genericResponse.As<RustBridgeResponseError>() };

            return response;
        }
    }
}
