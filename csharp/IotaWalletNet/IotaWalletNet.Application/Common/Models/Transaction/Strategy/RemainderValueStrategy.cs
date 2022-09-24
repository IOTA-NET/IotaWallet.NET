namespace IotaWalletNet.Application.Common.Models.Transaction.Strategy
{
    public abstract class RemainderValueStrategy
    {
        public RemainderValueStrategy(string strategy, string? value)
        {
            Strategy = strategy;
            Value = value;
        }

        public string Strategy { get; }
        public string? Value { get; }
    }
}
