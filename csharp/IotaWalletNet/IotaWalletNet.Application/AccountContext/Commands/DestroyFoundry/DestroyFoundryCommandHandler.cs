using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.DestroyFoundry
{
    public class DestroyFoundryCommandHandler : IRequestHandler<DestroyFoundryCommand, DestroyFoundryResponse>
    {
        public async Task<DestroyFoundryResponse> Handle(DestroyFoundryCommand request, CancellationToken cancellationToken)
        {
            DestroyFoundryCommandMessageData messageData = new DestroyFoundryCommandMessageData(request.FoundryId);

            DestroyFoundryCommandMessage message = new DestroyFoundryCommandMessage(request.Username, messageData);

            string messageJson = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(messageJson);

            DestroyFoundryResponse response = genericResponse.IsSuccess
                                                ? genericResponse.As<DestroyFoundryResponse>()!
                                                : new DestroyFoundryResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };

            return response;
        }
    }
}
