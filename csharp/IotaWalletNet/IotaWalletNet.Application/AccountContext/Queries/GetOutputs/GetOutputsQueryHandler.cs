﻿using IotaWalletNet.Domain.PlatformInvoke;
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

            GetOutputsResponse response = genericResponse.As<GetOutputsResponse>()!;

            return response;
        }
    }
}
