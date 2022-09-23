using IotaWalletNet.Domain.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IotaWalletNet.Domain.Common.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors
                .AddTransient<IWallet, Wallet>();

            return serviceDescriptors;
        }
    }
}
