using EnumsNET;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes;
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
            dynamic parsedJson = JsonConvert.DeserializeObject(message);
            string s = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            Console.WriteLine(s);
            IWalletEvent? walletEvent = JsonConvert.DeserializeObject<WalletEvent>(message);
            WalletEventReceived?.Invoke(this, walletEvent);
        }

        public void WalletMessageReceivedCallback(string message, string error, IntPtr context)
        {
            bool isSuccess = String.IsNullOrEmpty(error) && !message.Contains(@"{""type"":""error");

            string messageToSignal = String.IsNullOrEmpty(error) ? message : error;

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

            TaskCompletionSource<RustBridgeGenericResponse>? taskCompletionSource = 
                new TaskCompletionSource<RustBridgeGenericResponse>();
            
            Task<RustBridgeGenericResponse>? task = taskCompletionSource.Task;
            _messageReceivedCallbackSetResult = taskCompletionSource.SetResult;

            await Task.Factory.StartNew(() =>
            {
                RustBridge.SendMessageToRust(_walletHandle, message, _messageReceivedCallback, IntPtr.Zero);
            }, TaskCreationOptions.AttachedToParent);


            return await task;
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
