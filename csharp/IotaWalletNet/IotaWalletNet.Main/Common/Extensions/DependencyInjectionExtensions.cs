using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IotaWalletNet.Main.Common.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMainServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMediatR(Assembly.GetExecutingAssembly());

            return serviceDescriptors;
        }
    }
}
