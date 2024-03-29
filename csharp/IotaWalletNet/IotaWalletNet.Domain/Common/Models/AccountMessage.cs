﻿namespace IotaWalletNet.Domain.Common.Models
{
    public class AccountMessage : Message<AccountPayload>
    {
        public AccountMessage(string username, string payloadMethodName)
        {
            Cmd = "callAccountMethod";
            Payload = new AccountPayload(username, payloadMethodName);
        }
    }

    /// <summary>
    /// The message to be used for account methods.
    /// </summary>
    /// <typeparam name="T">The type of payload method data</typeparam>
    public class AccountMessage<T> : Message<AccountPayload<T>>
    {
        public AccountMessage(string username, string payloadMethodName, T? methodData)
        {
            Cmd = "callAccountMethod";
            Payload = new AccountPayload<T>(username, payloadMethodName, methodData);
        }
    }

}
