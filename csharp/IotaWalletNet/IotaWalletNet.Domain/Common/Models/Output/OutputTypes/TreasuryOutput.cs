using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Output.OutputTypes
{
    public class TreasuryOutput : IOutputType
    {
        public TreasuryOutput(string amount)
        {
            Amount = amount;
        }

        public int Type { get; } = 2;

        /// <summary>
        /// The amount of the output.   
        /// </summary>
        public string Amount { get; set; }
    }
}
