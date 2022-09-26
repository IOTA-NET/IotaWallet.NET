using IotaWalletNet.Domain.Common.Interfaces;
using static IotaWalletNet.Domain.PlatformInvoke.RustBridge;

namespace IotaWalletNet.Domain.PlatformInvoke
{
    public abstract class RustBridgeCommunicator : IRustBridgeCommunicator
    {
        protected MessageReceivedCallback _messageReceivedCallback;
        protected Action<string>? _endOfCallbackSignaller;
        protected IntPtr _walletHandle;

        public RustBridgeCommunicator()
        {
            _messageReceivedCallback = WalletMessageReceivedCallback;
            _endOfCallbackSignaller = null;
        }

        public void WalletMessageReceivedCallback(string message, string error, IntPtr context)
        {
            string messageToSignal = String.IsNullOrEmpty(message) ? error : message;

            if (_endOfCallbackSignaller != null)
                _endOfCallbackSignaller(messageToSignal);
        }

        public async Task<string> SendMessageAsync(string message)
        {

            var taskCompletionSource = new TaskCompletionSource<string>();
            var task = taskCompletionSource.Task;
            _endOfCallbackSignaller = taskCompletionSource.SetResult;

            await Task.Factory.StartNew(() =>
            {
                RustBridge.SendMessageToRust(_walletHandle, message, _messageReceivedCallback, IntPtr.Zero);
            }, TaskCreationOptions.AttachedToParent);


            return await task;
        }
    }
}
