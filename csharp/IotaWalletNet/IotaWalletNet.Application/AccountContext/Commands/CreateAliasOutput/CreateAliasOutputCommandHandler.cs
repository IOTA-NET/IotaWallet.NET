using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.CreateAliasOutput
{
    public class CreateAliasOutputCommandHandler : IRequestHandler<CreateAliasOutputCommand, CreateAliasOutputResponse>
    {
        public async Task<CreateAliasOutputResponse> Handle(CreateAliasOutputCommand request, CancellationToken cancellationToken)
        {
            CreateAliasOutputCommandMessageData messageData = new CreateAliasOutputCommandMessageData(request.AliasOutputOptions, new TransactionOptions());
            CreateAliasOutputCommandMessage message = new CreateAliasOutputCommandMessage(request.Username, messageData);
            string messageJson = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(messageJson);

            CreateAliasOutputResponse response = genericResponse.As<CreateAliasOutputResponse>()!;

            return response;
        }
    }
}
