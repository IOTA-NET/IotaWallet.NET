using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.UnlockConditionTypes
{
    public class AddressUnlockCondition : IUnlockConditionType
    {
        public AddressUnlockCondition(IAddressType addressType)
        {
            Address = addressType;
        }

        public int Type { get; } = 0;

        /// <summary>
        /// The Address
        /// </summary>
        public IAddressType Address { get; set; }
    }
}
