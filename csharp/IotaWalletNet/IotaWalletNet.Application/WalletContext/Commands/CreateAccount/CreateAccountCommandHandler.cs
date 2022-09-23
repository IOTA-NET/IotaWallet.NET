using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.WalletContext.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
    {
        public Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            CreateAccountCommandMessage message = new CreateAccountCommandMessage(request.Username);
            string messageJson = JsonConvert.SerializeObject(message);

            request.Wallet.SendMessage(messageJson);

            return Unit.Task;
        }
    }
}
