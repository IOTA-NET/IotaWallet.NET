using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetMinimumStorageDepositRequired
{
    public class GetMinimumStorageDepositRequiredQueryHandler : IRequestHandler<GetMinimumStorageDepositRequiredQuery, GetMinimumStorageDepositRequiredResponse>
    {
        public async Task<GetMinimumStorageDepositRequiredResponse> Handle(GetMinimumStorageDepositRequiredQuery request, CancellationToken cancellationToken)
        {
            GetMinimumStorageDepositRequiredQueryMessage message = new GetMinimumStorageDepositRequiredQueryMessage(request.Username, request.OutputType);
            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            GetMinimumStorageDepositRequiredResponse response = genericResponse.IsSuccess
                                                                    ? genericResponse.As<GetMinimumStorageDepositRequiredResponse>()!
                                                                    : new GetMinimumStorageDepositRequiredResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };

            return response;
        }
    }
}
