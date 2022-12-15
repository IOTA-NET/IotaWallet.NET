namespace IotaWalletNet.Application.AccountContext.Commands.ConsolidateOutputs
{
    public class ConsolidateOutputsCommandMessageData
    {
        public ConsolidateOutputsCommandMessageData(bool force, int? outputConsolidationThreshold = null)
        {
            Force = force;
            OutputConsolidationThreshold = outputConsolidationThreshold;
        }

        public bool Force { get; set; }

        public int? OutputConsolidationThreshold { get; set; }
    }
}
