using IotaWalletNet.Domain.Common.Models;
using IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes;

namespace IotaWalletNet.Application.AccountContext.Commands.BuildNftOutput
{
    internal class BuildNftOutputCommandMessage : AccountMessage<BuildNftOutputData>
    {
        private const string METHOD_NAME = "buildNftOutput";
        public BuildNftOutputCommandMessage(string username, BuildNftOutputData? methodData)
            : base(username, METHOD_NAME, methodData)
        {
        }
    }
}
