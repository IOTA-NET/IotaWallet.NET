using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetAddressesWithUnspentOutputs
{
    public class GetAddressesWithUnspentOutputsQueryHandler : IRequestHandler<GetAddressesWithUnspentOutputsQuery, GetAddressesWithUnspentOutputsResponse>
    {
        public async Task<GetAddressesWithUnspentOutputsResponse> Handle(GetAddressesWithUnspentOutputsQuery request, CancellationToken cancellationToken)
        {
            GetAddressesWithUnspentOutputsQueryMessage message = new GetAddressesWithUnspentOutputsQueryMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            GetAddressesWithUnspentOutputsResponse response = genericResponse.As<GetAddressesWithUnspentOutputsResponse>()!;

            return response;
        }
    }
}
