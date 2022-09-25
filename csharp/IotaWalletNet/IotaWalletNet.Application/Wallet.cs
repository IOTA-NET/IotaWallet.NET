using IotaWalletNet.Application.Common.Notifications;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Options;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using static IotaWalletNet.Domain.PlatformInvoke.RustBridge;

namespace IotaWalletNet.Application
{
    public class Wallet : IWallet, IDisposable
    {

        #region Variables

        private IntPtr _walletHandle;

        private StringBuilder _errorBuffer;

        private WalletOptions _walletOptions;

        private readonly IMediator _mediator;

        private MessageReceivedCallback _messageReceivedCallback;
        #endregion

        public Wallet(IMediator mediator)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            _walletOptions = new WalletOptions();

            _walletHandle = IntPtr.Zero;

            _errorBuffer = new StringBuilder();
            _mediator = mediator;

            _messageReceivedCallback = WalletMessageReceivedCallback;
        }

        
        public void WalletMessageReceivedCallback(string message, string error, IntPtr context)
        {
            //_mediator.Publish(new MessageReceivedNotification(message, error)).Wait();
            _endtaskcallback(message);
        }

        public void Connect()
        {
            string walletOptions = JsonConvert.SerializeObject(GetWalletOptions());

            int errorBufferSize = 1024;

            _errorBuffer = new StringBuilder(errorBufferSize);

            _walletHandle = InitializeIotaWallet(walletOptions, _errorBuffer, errorBufferSize);

            if (!string.IsNullOrEmpty(_errorBuffer.ToString()))
                throw new Exception(_errorBuffer.ToString());

        }
        public void SendMessage(string message)
        {
            SendMessageToRust(_walletHandle, message, _messageReceivedCallback, IntPtr.Zero);
        }
        private Action<string> _endtaskcallback;
        public async Task<string?> SendMessageAsync(string message)
        {
            
            var taskCompletionSource = new TaskCompletionSource<string>();
            var task = taskCompletionSource.Task;
            _endtaskcallback = taskCompletionSource.SetResult;

            await Task.Factory.StartNew(() =>
            {
                SendMessage(message);
            }, TaskCreationOptions.AttachedToParent);


            return await task;
        }

        public WalletOptions GetWalletOptions() => _walletOptions;


        public ClientOptionsBuilder ConfigureClientOptions()
            => new ClientOptionsBuilder(this);


        public SecretManagerOptionsBuilder ConfigureSecretManagerOptions()
            => new SecretManagerOptionsBuilder(this);

        public WalletOptionsBuilder ConfigureWalletOptions()
            => new WalletOptionsBuilder(this);

        public void Dispose()
        {
            CloseIotaWallet(_walletHandle);
            _walletHandle = IntPtr.Zero;
        }
    }
}
