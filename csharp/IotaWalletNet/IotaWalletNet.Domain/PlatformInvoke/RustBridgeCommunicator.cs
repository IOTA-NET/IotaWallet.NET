using IotaWalletNet.Domain.Common.Interfaces;
using static IotaWalletNet.Domain.PlatformInvoke.RustBridge;

namespace IotaWalletNet.Domain.PlatformInvoke
{
    public abstract class RustBridgeCommunicator : IRustBridgeCommunicator
    {
        protected MessageReceivedCallback _messageReceivedCallback;
        protected Action<RustBridgeGenericResponse>? _endOfCallbackSignaller;
        protected IntPtr _walletHandle;

        public RustBridgeCommunicator()
        {
            _messageReceivedCallback = WalletMessageReceivedCallback;
            _endOfCallbackSignaller = null;
        }

        public RustBridgeCommunicator(IntPtr walletHandle)
            : this()
        {
            _walletHandle = walletHandle;
        }
        public void WalletMessageReceivedCallback(string message, string error, IntPtr context)
        {
            bool isSuccess = String.IsNullOrEmpty(error) && !message.Contains(@"{""type"":""error");

            string messageToSignal = String.IsNullOrEmpty(error) ? message : error;

            if (_endOfCallbackSignaller != null)
                _endOfCallbackSignaller(new RustBridgeGenericResponse(messageToSignal, isSuccess));
        }

        /// <summary>
        /// Sends messages to the rust message interface. 
        /// In order to follow a async programming paradigm, we create a TaskCompletion Source internally.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<RustBridgeGenericResponse> SendMessageAsync(string message)
        {

            var taskCompletionSource = new TaskCompletionSource<RustBridgeGenericResponse>();
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
