using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResponse>
    {
        public async Task<CreateAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            CreateAccountCommandMessage message = new CreateAccountCommandMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Wallet.SendMessageAsync(json);

            CreateAccountResponse response = genericResponse.IsSuccess
                                            ? genericResponse.As<CreateAccountResponse>()!
                                            : new CreateAccountResponse() { Error = genericResponse.As<RustBridgeResponseError>(), Type = "error" };


            return response;
        }
    }
}
