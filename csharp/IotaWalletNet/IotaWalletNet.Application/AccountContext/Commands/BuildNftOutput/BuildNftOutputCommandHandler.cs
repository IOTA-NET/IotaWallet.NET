using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.BuildNftOutput
{
    internal class BuildNftOutputCommandHandler : IRequestHandler<BuildNftOutputCommand, BuildNftOutputResponse>
    {
        public async Task<BuildNftOutputResponse> Handle(BuildNftOutputCommand request, CancellationToken cancellationToken)
        {
            BuildNftOutputCommandMessage message = new BuildNftOutputCommandMessage(request.Username, request.Data);
            
            string jsonMessage = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse rustBridgeGenericResponse = await request.Account.SendMessageAsync(jsonMessage);

            BuildNftOutputResponse buildNftOutputResponse = rustBridgeGenericResponse.As<BuildNftOutputResponse>()!;

            return buildNftOutputResponse;
        }
    }
}
