using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetUnspentOutputs
{
    public class GetUnspentOutputsQueryHandler : IRequestHandler<GetUnspentOutputsQuery, GetUnspentOutputsResponse>
    {
        public async Task<GetUnspentOutputsResponse> Handle(GetUnspentOutputsQuery request, CancellationToken cancellationToken)
        {
            GetUnspentOutputsQueryMessage message = new GetUnspentOutputsQueryMessage(request.Username, new GetUnspentOutputsQueryMessageData());

            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            GetUnspentOutputsResponse response = genericResponse.As<GetUnspentOutputsResponse>()!;
            return response;
        }
    }
}
