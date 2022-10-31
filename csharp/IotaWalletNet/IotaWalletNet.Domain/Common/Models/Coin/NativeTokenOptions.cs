namespace IotaWalletNet.Domain.Common.Models.Coin
{
    public class NativeTokenOptions
    {
        public string? AliasId { get; set; }

        /// <summary>
        /// [HexEncoded Amount] 
        /// </summary>
        public string CirculatingSupply { get; set; }

        /// <summary>
        /// [HexEncoded Amount]
        /// </summary>
        public string MaximumSupply { get; set; }

        /// <summary>
        /// [HexEncoded]
        /// </summary>
        public string? FoundryMetadata { get; set; }
    }
}
