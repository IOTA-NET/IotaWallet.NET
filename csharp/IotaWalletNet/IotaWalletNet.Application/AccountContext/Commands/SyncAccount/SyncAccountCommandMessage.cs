﻿using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.SyncAccount
{
    public class SyncAccountCommandMessage : AccountMessage<AccountSyncOptions>
    {
        private const string METHOD_NAME = "syncAccount";
        public SyncAccountCommandMessage(string username, AccountSyncOptions accountSyncOptions)
            : base(username, METHOD_NAME, accountSyncOptions)
        {
        }
    }
}
