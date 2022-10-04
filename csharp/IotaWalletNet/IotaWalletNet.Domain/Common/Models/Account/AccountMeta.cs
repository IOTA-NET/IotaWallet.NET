using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Output;

namespace IotaWalletNet.Domain.Common.Models.Account
{
    public class AccountMeta
    {
        public int Index { get; set; }

        public TypeOfCoin CoinType { get; set; }

        public string Alias { get; set; } = string.Empty;

        public List<AccountAddress> PublicAddresses { get; set; } = new List<AccountAddress>();
        public List<AccountAddress> InternalAddresses { get; set; } = new List<AccountAddress>();

        public List<AddressWithUnspentOutputs> AddressesWithUnspentOutputs { get; set; } = new List<AddressWithUnspentOutputs>();

        public Dictionary<string, OutputData> Outputs { get; set; } = new Dictionary<string, OutputData>();

        /// <summary>
        ///  Output IDs of unspent outputs that are currently used as input for transactions
        /// </summary>
        public HashSet<string> LockedOutputs { get; set; } = new HashSet<string>();

        public Dictionary<string, OutputData> UnspentOutputs { get; set; } = new Dictionary<string, OutputData>();


        //public List<string> Transactions { get; set; } = new List<string>();

        public HashSet<string> PendingTransactions { get; set; } = new HashSet<string>();


    }
}
