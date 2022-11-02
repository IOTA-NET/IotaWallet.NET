using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Output;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.CreateAliasOutput
{
    
    public class CreateAliasOutputCommand : IRequest<CreateAliasOutputResponse>
    {
        public CreateAliasOutputCommand(string username, IAccount account, AliasOutputOptions aliasOutputOptions)
        {
            Username = username;
            Account = account;
            AliasOutputOptions = aliasOutputOptions;
        }

        public string Username { get; set; }
        public IAccount Account { get; set; }

        public AliasOutputOptions AliasOutputOptions { get; set; }
    }
}
