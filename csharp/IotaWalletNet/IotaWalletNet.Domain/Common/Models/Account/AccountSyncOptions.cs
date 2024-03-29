﻿namespace IotaWalletNet.Domain.Common.Models.Account
{
    public class AccountSyncOptions
    {
        /**
        * Specific Bech32 encoded addresses of the account to sync, if addresses are provided,
        * then `address_start_index` will be ignored
        */
        public List<string> Addresses { get; set; } = new List<string>();

        /**
         * Address index from which to start syncing addresses. 0 by default, using a higher index will be faster because
         * addresses with a lower index will be skipped, but could result in a wrong balance for that reason
         */
        public int AddressStartIndex { get; set; } = 0;

        /**
         * Address index from which to start syncing internal addresses. 0 by default, using a higher index will be faster
         * because addresses with a lower index will be skipped, but could result in a wrong balance for that reason
         */
        public int AddressStartIndexInternal { get; set; } = 0;

        /**
        * Usually syncing is skipped if it's called in between 200ms, because there can only be new changes every
        * milestone and calling it twice "at the same time" will not return new data
        * When this to true, we will sync anyways, even if it's called 0ms after the las sync finished. Default: false.
        */
        public bool ForceSyncing { get; set; } = false;

        // Try to sync transactions from incoming outputs with their inputs.
        // Some data may not be obtained if it has been pruned.
        public bool SyncIncomingTransactions { get; set; } = true;

        /** Checks pending transactions and promotes/reattaches them if necessary.  Default: true. */
        public bool SyncPendingTransactions { get; set; } = true;

        public bool SyncAliasesAndNfts { get; set; } = true;

        /** Specifies if only basic outputs with an AddressUnlockCondition alone should be synced, will overwrite
          * `syncAliasesAndNfts`. Default: false. 
        */
        public bool SyncOnlyMostBasicOutputs { get; set; } = false;
    }
}
