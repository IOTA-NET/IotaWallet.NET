using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IotaWalletNet.Application.Common.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMediatR(Assembly.GetExecutingAssembly());

            return serviceDescriptors;
        }
    }
}
