using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.ClaimOutputs
{
    public class ClaimOutputsCommandHandler : IRequestHandler<ClaimOutputsCommand, ClaimOutputsResponse>
    {
        public async Task<ClaimOutputsResponse> Handle(ClaimOutputsCommand request, CancellationToken cancellationToken)
        {
            ClaimOutputsCommandMessageData messageData = new ClaimOutputsCommandMessageData(request.OutputIds);
            ClaimOutputsCommandMessage message = new ClaimOutputsCommandMessage(request.Username, messageData);
            string messageJson = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(messageJson);

            ClaimOutputsResponse response = genericResponse.As<ClaimOutputsResponse>()!;
            return response;
        }
    }
}
