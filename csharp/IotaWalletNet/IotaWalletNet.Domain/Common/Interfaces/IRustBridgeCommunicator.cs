﻿using IotaWalletNet.Domain.Common.Models.Events;
using IotaWalletNet.Domain.PlatformInvoke;
using static IotaWalletNet.Domain.Common.Models.Events.EventTypes;

namespace IotaWalletNet.Domain.Common.Interfaces
{
    public interface IRustBridgeCommunicator
    {
        event EventHandler<IWalletEvent?>? WalletEventReceived;

        Task<RustBridgeGenericResponse> SendMessageAsync(string message);
        void SubscribeToEvents(WalletEventTypes walletEventTypes);
        void WalletMessageReceivedCallback(string message, string error, IntPtr context);
    }
}