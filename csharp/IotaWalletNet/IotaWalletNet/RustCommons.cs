using System.Runtime.InteropServices;

namespace IotaWalletNet
{
    public static class RustCommons
    {
        [DllImport("bindings", EntryPoint = "free_c_string", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void FreeCString(IntPtr c_str);
    }
}
