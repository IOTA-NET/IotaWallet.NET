using IotaWalletNet.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.InteropServices;
using System.Text;

namespace IotaWalletNet
{
    public class Wallet
    {
        //[DllImport("bindings", EntryPoint = "create_wallet_manager", CallingConvention = CallingConvention.Cdecl)]
        //private static extern IntPtr CreateWalletManager(string password, string nodeUrl, UInt32 coinType);

        //[DllImport("bindings", EntryPoint = "create_account", CallingConvention = CallingConvention.Cdecl)]
        //private static extern void CreateAccount(IntPtr accountManagerHandle, string accountName);

        //[DllImport("bindings", EntryPoint = "get_usernames", CallingConvention = CallingConvention.Cdecl)]
        //private static extern string GetUsernames(IntPtr accountManagerHandle);

        //[DllImport("bindings", EntryPoint = "get_account", CallingConvention = CallingConvention.Cdecl)]
        //private static extern IntPtr GetAccount(IntPtr accountManagerHandle, string username);

        [DllImport("bindings", EntryPoint = "iota_initialize", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr InitializeIotaWallet(string managerOptions, [MarshalAs(UnmanagedType.LPStr)] StringBuilder errorBuffer, int errorBufferSize);

        [DllImport("bindings", EntryPoint = "iota_send_message", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SendMessageToRust(IntPtr walletHandle, string message, [MarshalAs(UnmanagedType.FunctionPtr)] MyCallback callback, IntPtr context);

        private delegate void MyCallback(string message, string errors, IntPtr context);

        private MyCallback _callback =  (message, error, context) =>
        {
            Console.WriteLine(error);
        };


        private IntPtr _walletHandle;

        private StringBuilder _errorBuffer;

        private WalletOptions _walletOptions;

        public WalletOptions GetWalletOptions() => _walletOptions;
        
        public void SendMessage(string message)
        {
            SendMessageToRust(_walletHandle, message, _callback, IntPtr.Zero);
        }
        public Wallet()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            _walletOptions = new WalletOptions();

            _walletHandle = IntPtr.Zero;

            _errorBuffer = new StringBuilder();
        }

        public ClientOptionsBuilder ConfigureClientOptions()
            => new ClientOptionsBuilder(this);


        public SecretManagerOptionsBuilder ConfigureSecretManagerOptions()
            => new SecretManagerOptionsBuilder(this);

        public WalletOptionsBuilder ConfigureWalletOptions()
            => new WalletOptionsBuilder(this);

        public void Connect()
        {
            string walletOptions = JsonConvert.SerializeObject(GetWalletOptions());

            int errorBufferSize = 1024;

            _errorBuffer = new StringBuilder(errorBufferSize);

            _walletHandle = Wallet.InitializeIotaWallet(walletOptions, _errorBuffer, errorBufferSize);

            if (!string.IsNullOrEmpty(_errorBuffer.ToString()))
                throw new Exception(_errorBuffer.ToString());

        }
    }
}
