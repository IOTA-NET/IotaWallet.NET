using IotaWalletNet.Application.Common.HttpClients;
using Refit;

namespace IotaWalletNet.Application.Common.Interfaces
{

    [Headers("Content-Type: application/json")]
    public interface IFaucetApi
    {
        [Post("/api/enqueue")]
        Task RequestFromFaucet([Body] RequestFromFaucetModel request);
    }
}
