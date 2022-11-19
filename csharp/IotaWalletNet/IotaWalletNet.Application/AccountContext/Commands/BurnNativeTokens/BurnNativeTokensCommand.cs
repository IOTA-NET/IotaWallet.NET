using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.BurnNativeTokens
{
    public class BurnNativeTokensCommand : IRequest<BurnNativeTokensResponse>
    {
        public BurnNativeTokensCommand(string tokenId, string meltAmount, string username, IAccount account)
        {
            TokenId = tokenId;
            MeltAmount = meltAmount;
            Username = username;
            Account = account;
        }

        /// <summary>
        /// The native token id.
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// [HexEncodedAmount] To be melted amount.
        /// </summary>
        public string MeltAmount { get; set; }


        public string Username { get; set; }

        public IAccount Account { get; set; }
    }
}
