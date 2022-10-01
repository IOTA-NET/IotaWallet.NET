using System.Runtime.InteropServices;
using System.Text;

namespace IotaWalletNet.Domain.PlatformInvoke
{
    public static class RustBridge
    {
        public delegate void MessageReceivedCallback(string message, string errors, IntPtr context);


        [DllImport("bindings", EntryPoint = "iota_initialize", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr InitializeIotaWallet(string managerOptions, [MarshalAs(UnmanagedType.LPStr)] StringBuilder errorBuffer, int errorBufferSize);

        [DllImport("bindings", EntryPoint = "iota_send_message", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SendMessageToRust(IntPtr walletHandle, string message, [MarshalAs(UnmanagedType.FunctionPtr)] MessageReceivedCallback callback, IntPtr context);

        [DllImport("bindings", EntryPoint = "iota_destroy", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseIotaWallet(IntPtr walletHandle);



    }
}
