using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Queries.GetOutputsWithAdditionalUnlockConditions
{
    public class GetOutputsWithAdditionalUnlockConditionsQueryHandler : IRequestHandler<GetOutputsWithAdditionalUnlockConditionsQuery, GetOutputsWithAdditionalUnlockConditionsResponse>
    {
        public async Task<GetOutputsWithAdditionalUnlockConditionsResponse> Handle(GetOutputsWithAdditionalUnlockConditionsQuery request, CancellationToken cancellationToken)
        {
            GetOutputsWithAdditionalUnlockConditionsMessageData messageData = new GetOutputsWithAdditionalUnlockConditionsMessageData(request.OutputsTypeToClaim);
            GetOutputsWithAdditionalUnlockConditionsMessage message = new GetOutputsWithAdditionalUnlockConditionsMessage(request.Username, messageData);
            string messageJson = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(messageJson);
            GetOutputsWithAdditionalUnlockConditionsResponse response = genericResponse.As<GetOutputsWithAdditionalUnlockConditionsResponse>()!;
            
            return response;
        }
    }
}
