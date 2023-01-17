using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Address.AddressTypes;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Network;
using IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes;
using IotaWalletNet.Domain.Common.Models.Output.UnlockConditionTypes;

namespace IotaWalletNet.Application.Common.Builders
{
    public class BuildBasicOutputDataUnlockConditionsBuilder
    {
        private readonly BuildBasicOutputBuilder _buildBasicOutputDataBuilder;
        private readonly BuildBasicOutputData _outputData;

        public BuildBasicOutputDataUnlockConditionsBuilder(BuildBasicOutputBuilder buildBasicOutputDataBuilder, BuildBasicOutputData outputData)
        {
            _buildBasicOutputDataBuilder = buildBasicOutputDataBuilder;
            _outputData = outputData;
        }

        public BuildBasicOutputDataUnlockConditionsBuilder SetAddressUnlockConditionUsingBech32(string bech32Address)
        {
            (NetworkType networkType, TypeOfCoin typeOfCoin) = HumanReadablePart.DetermineNetworkAndTypeOfCoin(bech32Address);
            string ed25519Address = bech32Address.DecodeBech32IntoEd25519Hash(networkType, typeOfCoin);
            AddressUnlockCondition addressUnlockCondition = new AddressUnlockCondition(new Ed25519Address(ed25519Address));

            _outputData.UnlockConditions.Add(addressUnlockCondition);

            return this;
        }

        public BuildBasicOutputBuilder Then() => _buildBasicOutputDataBuilder;
    }
}
