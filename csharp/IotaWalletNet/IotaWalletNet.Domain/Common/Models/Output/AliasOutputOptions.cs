namespace IotaWalletNet.Domain.Common.Models.Output
{
    /// <summary>
    /// Options for the alias output creation
    /// </summary>
    public class AliasOutputOptions
    {
        /// <summary>
        ///  Bech32 encoded address to which the Nft will be minted. Default will use the
        /// first address of the account
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// [HexEncoded]
        /// </summary>
        public string? ImmutableMetadata { get; set; }

        /// <summary>
        /// [HexEncoded]
        /// </summary>
        public string? Metadata { get; set; }

        /// <summary>
        /// [HexEncoded]
        /// </summary>
        public string? StateMetadata { get; set; }
    }
}
