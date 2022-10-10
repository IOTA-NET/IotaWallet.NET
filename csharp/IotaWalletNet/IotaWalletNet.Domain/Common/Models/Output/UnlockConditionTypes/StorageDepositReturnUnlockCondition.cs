using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockConditionTypes
{
    public class StorageDepositReturnUnlockCondition : IUnlockConditionType
    {
        public StorageDepositReturnUnlockCondition(IAddressType addressType, string amount)
        {
            Address = addressType;
            Amount = amount;
        }

        public int Type { get; } = 1;

        /// <summary>
        /// The return address
        /// </summary>
        public IAddressType Address { get; set; }

        /// <summary>
        /// Amount of IOTA tokens the consuming transaction should deposit to the address defined in return address.
        /// </summary>
        public string Amount { get; set; }
    }
}
