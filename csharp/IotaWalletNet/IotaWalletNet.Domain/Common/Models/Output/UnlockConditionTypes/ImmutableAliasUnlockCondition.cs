using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockConditionTypes
{
    public class ImmutableAliasUnlockCondition : IUnlockConditionType
    {
        public ImmutableAliasUnlockCondition(IAddressType addressType)
        {
            Address = addressType;
        }

        public int Type { get; } = 6;

        /// <summary>
        /// The Address
        /// </summary>
        public IAddressType Address { get; set; }
    }
}
