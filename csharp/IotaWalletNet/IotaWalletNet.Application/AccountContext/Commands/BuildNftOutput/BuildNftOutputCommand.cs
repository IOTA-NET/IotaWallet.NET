using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Output.OutputDataTypes;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.BuildNftOutput
{
    internal class BuildNftOutputCommand : IRequest<BuildNftOutputResponse>
    {
        public BuildNftOutputCommand(BuildNftOutputData data, string username, IAccount account)
        {
            Data = data;
            Username = username;
            Account = account;
        }

        public BuildNftOutputData Data { get; }
        public string Username { get; }
        public IAccount Account { get; }
    }
}
