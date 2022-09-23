﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IotaWalletNet.Testbed.Common.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddConsoleServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMediatR(Assembly.GetExecutingAssembly());

            return serviceDescriptors;
        }
    }
}