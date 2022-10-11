using IotaWalletNet.Domain.Common.Interfaces;

namespace IotaWalletNet.Domain.Common.Models.Coin.TokenSchemeTypes
{
    public class SimpleTokenScheme : ITokenSchemeType
    {
        public SimpleTokenScheme(string mintedTokens, string meltedTokens, string maximumSupply)
        {
            MintedTokens = mintedTokens;
            MeltedTokens = meltedTokens;
            MaximumSupply = maximumSupply;
        }


        public int Type { get; set; } = 0;

        /// <summary>
        /// Amount of tokens minted by this foundry.
        /// </summary>
        public string MintedTokens { get; set; }


        /// <summary>
        /// Amount of tokens melted by this foundry.
        /// </summary>
        public string MeltedTokens { get; set; }


        /// <summary>
        /// Maximum supply of tokens controlled by this foundry.
        /// </summary>
        public string MaximumSupply { get; set; }
    }
}
