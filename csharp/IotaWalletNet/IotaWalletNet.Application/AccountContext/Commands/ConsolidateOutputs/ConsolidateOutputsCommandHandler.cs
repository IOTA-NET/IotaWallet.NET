using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.ConsolidateOutputs
{
    public class ConsolidateOutputsCommandHandler : IRequestHandler<ConsolidateOutputsCommand, ConsolidateOutputsResponse>
    {
        public async Task<ConsolidateOutputsResponse> Handle(ConsolidateOutputsCommand request, CancellationToken cancellationToken)
        {
            ConsolidateOutputsCommandMessageData messageData = new ConsolidateOutputsCommandMessageData(request.Force, request.OutputConsolidationThreshold);
            ConsolidateOutputsCommandMessage message = new ConsolidateOutputsCommandMessage(request.Username, messageData);
            string messageJson = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(messageJson);
            ConsolidateOutputsResponse response = genericResponse.IsSuccess
                                                    ? genericResponse.As<ConsolidateOutputsResponse>()!
                                                    : new ConsolidateOutputsResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };

            return response;
        }
    }
}
