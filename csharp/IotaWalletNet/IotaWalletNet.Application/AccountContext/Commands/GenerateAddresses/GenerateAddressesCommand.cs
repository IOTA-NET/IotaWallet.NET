using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Network;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses
{
    public class GenerateAddressesCommand : IRequest<GenerateAddressesResponse>
    {
        public GenerateAddressesCommand(IAccount account, NetworkType networkType, string username, uint amount)
        {
            Account = account;
            NetworkType = networkType;
            Username = username;
            Amount = amount;
        }

        public IAccount Account { get; }
        public NetworkType NetworkType { get; }
        public string Username { get; }
        public uint Amount { get; }
    }
}
