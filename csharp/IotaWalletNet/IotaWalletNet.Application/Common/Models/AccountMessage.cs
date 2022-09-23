namespace IotaWalletNet.Application.Common.Models
{
    public class AccountMessage : Message<AccountPayload>
    {
        public AccountMessage()
        {
            Cmd = "CallAccountMethod";
        }
    }

    /// <summary>
    /// The message to be used for account methods.
    /// </summary>
    /// <typeparam name="T">The type of payload method data</typeparam>
    public class AccountMessage<T> : Message<AccountPayload<T>>
    {
        public AccountMessage()
        {
            Cmd = "CallAccountMethod";
        }
    }

}
