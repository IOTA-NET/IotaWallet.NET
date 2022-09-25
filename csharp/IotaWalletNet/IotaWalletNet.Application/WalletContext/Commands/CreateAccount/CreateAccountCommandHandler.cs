using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, string>
    {
        public async Task<string> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            CreateAccountCommandMessage message = new CreateAccountCommandMessage(request.Username);
            string json = JsonConvert.SerializeObject(message);

            string response = await request.Wallet.SendMessageAsync(json);

            return response;
        }
    }
}
