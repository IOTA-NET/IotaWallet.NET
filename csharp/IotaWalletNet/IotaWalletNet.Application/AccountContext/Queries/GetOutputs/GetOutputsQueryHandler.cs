using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetOutputs
{
    public class GetOutputsQueryHandler : IRequestHandler<GetOutputsQuery, GetOutputsResponse>
    {
        public async Task<GetOutputsResponse> Handle(GetOutputsQuery request, CancellationToken cancellationToken)
        {
            GetOutputsQueryMessageData messageData = new GetOutputsQueryMessageData(request.FilterOptions);
            GetOutputsQueryMessage message = new GetOutputsQueryMessage(request.Username, messageData);

            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            GetOutputsResponse response = genericResponse.IsSuccess
                                            ? genericResponse.As<GetOutputsResponse>()!
                                            : new GetOutputsResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };

            return response;
        }
    }
}
