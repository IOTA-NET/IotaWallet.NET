using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Output.InputTypes;
using IotaWalletNet.Domain.Common.Models.Output.OutputTypes;

namespace IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes
{
    public class TreasuryTransactionPayload : IPayloadType
    {
        public TreasuryTransactionPayload(TreasuryInput input, TreasuryOutput output)
        {
            Input = input;
            Output = output;
        }

        public int Type { get; } = 4;

        /// <summary>
        /// The input of this transaction.
        /// </summary>
        public TreasuryInput Input { get; set; }

        /// <summary>
        /// The output of this transaction.
        /// </summary>
        public TreasuryOutput Output { get; set; }
    }
}
