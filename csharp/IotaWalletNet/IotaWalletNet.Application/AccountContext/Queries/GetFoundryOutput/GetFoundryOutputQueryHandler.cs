using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetFoundryOutput
{
    public class GetFoundryOutputQueryHandler : IRequestHandler<GetFoundryOutputQuery, GetFoundryOutputResponse>
    {
        public async Task<GetFoundryOutputResponse> Handle(GetFoundryOutputQuery request, CancellationToken cancellationToken)
        {
            GetFoundryOutputQueryMessageData messageData = new GetFoundryOutputQueryMessageData(request.TokenId);
            GetFoundryOutputQueryMessage message = new GetFoundryOutputQueryMessage(request.Username, messageData);
            string messageJson = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(messageJson);

            GetFoundryOutputResponse getFoundryOutputResponse = genericResponse.IsSuccess
                                                                    ? genericResponse.As<GetFoundryOutputResponse>()!
                                                                    : new GetFoundryOutputResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };

            return getFoundryOutputResponse;
        }
    }
}
