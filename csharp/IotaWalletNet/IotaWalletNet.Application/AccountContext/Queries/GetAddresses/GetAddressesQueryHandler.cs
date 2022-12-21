using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetAddresses
{
    public class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, GetAddressesResponse>
    {
        public async Task<GetAddressesResponse> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            GetAddressesQueryMessage message = new GetAddressesQueryMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            GetAddressesResponse response = genericResponse.As<GetAddressesResponse>()!;

            return response;
        }
    }
}
