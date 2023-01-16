using IotaWalletNet.Domain.Common.Models;
using IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes;

namespace IotaWalletNet.Application.AccountContext.Commands.BuildBasicOutput
{
    public class BuildBasicOutputCommandMessage : AccountMessage<BuildBasicOutputData>
    {
        private const string METHOD_NAME = "buildBasicOutput";
        public BuildBasicOutputCommandMessage(string username, BuildBasicOutputData? methodData) 
            : base(username, METHOD_NAME, methodData)
        {
        }
    }
}
