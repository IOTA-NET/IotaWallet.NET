﻿using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses
{
    public class GenerateAddressesCommandHandler : IRequestHandler<GenerateAddressesCommand, GenerateAddressesResponse>
    {
        public async Task<GenerateAddressesResponse> Handle(GenerateAddressesCommand request, CancellationToken cancellationToken)
        {
            AddressGenerationOptions options = new AddressGenerationOptions()
            {
                Internal = false,
                Metadata = new GenerateAddressMetadata()
                {
                    Network = request.NetworkType,
                    Syncing = true
                }
            };

            GenerateAddressesCommandMessageData data = new GenerateAddressesCommandMessageData(request.Amount, options);

            GenerateAddressesCommandMessage message = new GenerateAddressesCommandMessage(request.Username, data);

            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            GenerateAddressesResponse response = genericResponse.As<GenerateAddressesResponse>()!;

            return response;
        }
    }
}
