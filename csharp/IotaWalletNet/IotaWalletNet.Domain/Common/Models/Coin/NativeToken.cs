namespace IotaWalletNet.Domain.Common.Models.Coin
{
    public class NativeToken
    {
        public NativeToken(string id, string amount)
        {
            Id = id;
            Amount = amount;
        }

        /// <summary>
        /// Identifier of the native token.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Amount of native tokens of the given Token ID.
        /// </summary>
        public string Amount { get; set; }
    }
}
