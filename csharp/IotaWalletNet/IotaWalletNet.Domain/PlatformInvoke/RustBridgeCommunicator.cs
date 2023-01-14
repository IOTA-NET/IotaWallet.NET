using EnumsNET;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes;
using IotaWalletNet.Domain.Common.Models.Logging;
using Newtonsoft.Json;
using System.Text;
using static IotaWalletNet.Domain.Common.Models.Events.EventTypes;
using static IotaWalletNet.Domain.PlatformInvoke.RustBridge;

namespace IotaWalletNet.Domain.PlatformInvoke
{
    public abstract class RustBridgeCommunicator : IRustBridgeCommunicator
    {
        protected MessageReceivedCallback _messageReceivedCallback;
        protected Action<RustBridgeGenericResponse>? _messageReceivedCallbackSetResult;

        protected EventReceivedCallback _eventReceivedCallback;
        public event EventHandler<IWalletEvent?>? WalletEventReceived;

        protected IntPtr _walletHandle;

        protected StringBuilder _eventErrorBuffer;
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public RustBridgeCommunicator()
        {
            _messageReceivedCallback = WalletMessageReceivedCallback;
            _messageReceivedCallbackSetResult = null;

            _eventReceivedCallback = WalletEventsReceivedCallback;
            _eventErrorBuffer = new StringBuilder();
        }


        public RustBridgeCommunicator(IntPtr walletHandle)
            : this()
        {
            _walletHandle = walletHandle;
        }

        public void WalletEventsReceivedCallback(string message, string error, IntPtr context)
        {
            IWalletEvent? walletEvent = JsonConvert.DeserializeObject<WalletEvent>(message);
            WalletEventReceived?.Invoke(this, walletEvent);
        }

        public void WalletMessageReceivedCallback(string message, string error, IntPtr context)
        {
            bool isSuccess = string.IsNullOrEmpty(error) && !message.Contains(@"{""type"":""error");

            string messageToSignal = string.IsNullOrEmpty(error) ? message : error;

            if (_messageReceivedCallbackSetResult != null)
                _messageReceivedCallbackSetResult(new RustBridgeGenericResponse(messageToSignal, isSuccess));
        }

        /// <summary>
        /// Sends messages to the rust message interface. 
        /// In order to follow a async programming paradigm, we create a TaskCompletion Source internally.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<RustBridgeGenericResponse> SendMessageAsync(string message)
        {
            await _semaphore.WaitAsync();

            TaskCompletionSource<RustBridgeGenericResponse>? taskCompletionSource =
                new TaskCompletionSource<RustBridgeGenericResponse>();

            Task<RustBridgeGenericResponse>? task = taskCompletionSource.Task;
            _messageReceivedCallbackSetResult = taskCompletionSource.SetResult;

            await Task.Factory.StartNew(() =>
            {
                RustBridge.SendMessageToRust(_walletHandle, message, _messageReceivedCallback, IntPtr.Zero);
            }, TaskCreationOptions.AttachedToParent);


            RustBridgeGenericResponse genericResponse = await task;

            _semaphore.Release();

            return genericResponse;
        }

        public byte EnableLogging(string filename, LogLevel logLevel)
        {
            return RustBridge.EnableLogging(filename, logLevel.ToString());
        }

        public void SubscribeToEvents(WalletEventTypes walletEventTypes)
        {
            List<string> eventNames = new List<string>();

            foreach (EnumMember<WalletEventTypes> walletEventMember in Enums.GetMembers<WalletEventTypes>())
            {
                if (walletEventMember.Value == WalletEventTypes.AllEvents)
                    continue;

                if (walletEventTypes.HasFlag(walletEventMember.Value))
                    eventNames.Add(walletEventMember.AsString());
            }

            string eventsAsJsonArray = JsonConvert.SerializeObject(eventNames);


            int errorBufferSize = 1024;

            _eventErrorBuffer = new StringBuilder(errorBufferSize);

            RustBridge.ListenForIotaWalletEvents(_walletHandle, eventsAsJsonArray, _eventReceivedCallback, IntPtr.Zero, _eventErrorBuffer, errorBufferSize);
        }
    }
}
