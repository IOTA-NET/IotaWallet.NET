using IotaWalletNet.Application.Common.Models;

namespace IotaWalletNet.Application.WalletContext.Commands.CreateAccount
{
    public class CreateAccountCommandMessage : Message<CreateAccountPayload>
    {
        private const string COMMAND = "CreateAccount";

        public CreateAccountCommandMessage(string username)
        {
            Cmd = COMMAND;
            Payload = new CreateAccountPayload(username);
        }

    }
}
