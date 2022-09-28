using IotaWalletNet.Domain.Common.Models.Address;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses
{
    public class GenerateAddressesCommandHandler : IRequestHandler<GenerateAddressesCommand, string>
    {
        public async Task<string> Handle(GenerateAddressesCommand request, CancellationToken cancellationToken)
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
            GenerateAddressesData data = new GenerateAddressesData(request.Amount, options);

            GenerateAddressesCommandMessage message = new GenerateAddressesCommandMessage(request.Username, data);
            string json = JsonConvert.SerializeObject(message);
            string response = await request.Account.SendMessageAsync(json);

            return response;
        }
    }
}
