using IotaWalletNet.Application.AccountContext.Commands.BuildBasicOutput;
using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes;

namespace IotaWalletNet.Application.Common.Builders
{
    public class BuildBasicOutputBuilder
    {
        private readonly IAccount _account;
        private readonly BuildBasicOutputData _outputData;

        public BuildBasicOutputBuilder(IAccount account)
        {
            _account = account;
            _outputData = new BuildBasicOutputData();
            Features = new BuildBasicOutputDataFeaturesBuilder(this, _outputData);
            UnlockConditions = new BuildBasicOutputDataUnlockConditionsBuilder(this, _outputData);
        }

        public BuildBasicOutputDataFeaturesBuilder Features { get; set; }

        public BuildBasicOutputDataUnlockConditionsBuilder UnlockConditions { get; set; }

        public BuildBasicOutputBuilder SetAmount(long amount)
        {
            return SetAmount(amount.ToString());
        }

        public BuildBasicOutputBuilder SetAmount(string amount)
        {
            _outputData.Amount = amount;

            return this;
        }

        public BuildBasicOutputBuilder SetNativeToken(string tokenId, string amount)
        {
            NativeToken nativeToken = new NativeToken(tokenId, amount);

            _outputData.NativeTokens = nativeToken;

            return this;
        }

        public BuildBasicOutputBuilder SetNativeToken(string tokenId, long amount)
        {
            return SetNativeToken(tokenId, amount.ToString());
        }

        public async Task<BuildBasicOutputResponse> BuildBasicOutputAsync()
        {
            return await _account.BuildBasicOutputAsync(_outputData);

        }
    }
}
