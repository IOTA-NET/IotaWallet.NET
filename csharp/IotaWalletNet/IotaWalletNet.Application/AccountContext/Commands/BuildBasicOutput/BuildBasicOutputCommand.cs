using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.BuildBasicOutput
{
    public class BuildBasicOutputCommand : IRequest<BuildBasicOutputResponse>
    {
        public BuildBasicOutputCommand(BuildBasicOutputData data, string username, IAccount account)
        {
            Data = data;
            Username = username;
            Account = account;
        }

        public BuildBasicOutputData Data { get; set; }

        public string Username { get; set; }

        public IAccount Account { get; set; }
    }
}
