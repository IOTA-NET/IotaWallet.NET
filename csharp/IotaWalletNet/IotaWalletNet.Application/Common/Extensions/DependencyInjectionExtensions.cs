using IotaWalletNet.Domain.Common.Extensions;
using IotaWalletNet.Domain.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IotaWalletNet.Application.Common.Extensions
{
    public static class DependencyInjectionExtensions
    {
        private static IServiceCollection AddApplicationServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient<IWallet, Wallet>();
            
            return serviceDescriptors;
        }

        public static IServiceCollection AddIotaWalletServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors
                .AddDomainServices()
                .AddApplicationServices()
                .AddMediatR(Assembly.GetExecutingAssembly());

            return serviceDescriptors;
        }
    }
}
