namespace IotaWalletNet.Application.WalletContext.Commands.CreateAccount
{
    public class CreateAccountPayload
    {
        public string Alias { get; }

        public CreateAccountPayload(string alias)
        {
            Alias = alias;
        }
    }
}
