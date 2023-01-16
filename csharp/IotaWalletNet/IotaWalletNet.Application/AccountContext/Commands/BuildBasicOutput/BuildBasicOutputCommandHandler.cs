using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.BuildBasicOutput
{
    public class BuildBasicOutputCommandHandler : IRequestHandler<BuildBasicOutputCommand, BuildBasicOutputResponse>
    {
        public async Task<BuildBasicOutputResponse> Handle(BuildBasicOutputCommand request, CancellationToken cancellationToken)
        {
            BuildBasicOutputCommandMessage message = new BuildBasicOutputCommandMessage(request.Username, request.Data);
            string jsonMessage = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse rustBridgeGenericResponse = await request.Account.SendMessageAsync(jsonMessage);

            BuildBasicOutputResponse buildBasicOutputResponse = rustBridgeGenericResponse.As<BuildBasicOutputResponse>()!;

            return buildBasicOutputResponse;
        }
    }
}
