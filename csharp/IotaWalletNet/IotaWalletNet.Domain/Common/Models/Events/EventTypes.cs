namespace IotaWalletNet.Domain.Common.Models.Events
{
    public static class EventTypes
    {
        [Flags]
        public enum WalletEventTypes
        {
            ConsolidationRequired = 0,
            NewOutput = 1,
            SpentOutput = 2,
            TransactionInclusion = 4,
            TransactionProgress = 8,
            AllEvents = ConsolidationRequired | NewOutput | SpentOutput | TransactionInclusion | TransactionProgress
        };


    }

}
