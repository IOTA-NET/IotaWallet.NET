using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Refit;
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

            serviceDescriptors
                .AddSingleton(faucetApiProvider => new Func<string, IFaucetApi>((url) => RestService.For<IFaucetApi>(url)));

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
