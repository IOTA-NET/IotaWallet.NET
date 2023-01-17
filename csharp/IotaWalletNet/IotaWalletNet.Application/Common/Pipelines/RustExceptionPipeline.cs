using IotaWalletNet.Application.Common.Exceptions;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.Common.Pipelines
{
    public class RustExceptionPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : RustBridgeResponseBase
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = await next();

            if (response.IsSuccess() == false)
            {
                string formattedError = JsonConvert.SerializeObject(response.Error, Formatting.Indented);
                throw new RustBridgeException($"Source: {typeof(TResponse).FullName}\n\n Error: {formattedError}");
            }

            return response;
        }
    }
}
