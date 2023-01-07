namespace IotaWalletNet.Application.AccountContext.Commands.ClaimOutputs
{
    public class ClaimOutputsCommandMessageData
    {
        public ClaimOutputsCommandMessageData(List<string> outputIdsToClaim)
        {
            OutputIdsToClaim = outputIdsToClaim;
        }

        public List<string> OutputIdsToClaim { get; set; }
    }
}
