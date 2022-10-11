using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Account
{
    public class MigratedFunds
    {
        public MigratedFunds(string tailTransactionHash, IAddressType address, string deposit)
        {
            TailTransactionHash = tailTransactionHash;
            Address = address;
            Deposit = deposit;
        }

        /// <summary>
        /// [HexEncoded] The tail transaction hash of the migration bundle.
        /// </summary>
        public string TailTransactionHash { get; set; }

        /// <summary>
        /// The target address of the migrated funds.
        /// </summary>
        public IAddressType Address { get; set; }

        /// <summary>
        /// The amount of the deposit.
        /// </summary>
        public string Deposit { get; set; }
    }
}
