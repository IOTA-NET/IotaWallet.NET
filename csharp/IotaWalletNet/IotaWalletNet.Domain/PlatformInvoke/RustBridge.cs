using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace IotaWalletNet.Domain.PlatformInvoke
{
    public static class RustBridge
    {

        #region Delegates

        public delegate void MessageReceivedCallback(string message, string errors, IntPtr context);


        public delegate void EventReceivedCallback(string message, string errors, IntPtr context);

        #endregion


        public static string ResolveLibraryNameFromPlatformType()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "iota_wallet.dll";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "libiota_wallet.so";
            else
                throw new NotSupportedException($"Only windows x64 and Linux x86_64 is supported");
        }
        public static IntPtr InitializeIotaWallet(string managerOptions, [MarshalAs(UnmanagedType.LPStr)] StringBuilder errorBuffer, int errorBufferSize)
        {
            Type[] paramTypes = { typeof(string), typeof(StringBuilder), typeof(int) };
            Object[] args = { managerOptions, errorBuffer, errorBufferSize };

            return (IntPtr)DynamicPInvokeBuilder(typeof(IntPtr), ResolveLibraryNameFromPlatformType(), "iota_initialize", args, paramTypes);
        }

        public static byte ListenForIotaWalletEvents(IntPtr walletHandle, string eventTypesAsJsonArray, [MarshalAs(UnmanagedType.FunctionPtr)] EventReceivedCallback callback, IntPtr context, [MarshalAs(UnmanagedType.LPStr)] StringBuilder errorBuffer, int errorBufferSize)
        {
            Type[] paramTypes = { typeof(IntPtr), typeof(string), typeof(EventReceivedCallback), typeof(IntPtr), typeof(StringBuilder), typeof(int) };
            Object[] args = { walletHandle, eventTypesAsJsonArray, callback, context, errorBuffer, errorBufferSize };

            return (byte)DynamicPInvokeBuilder(typeof(byte), ResolveLibraryNameFromPlatformType(), "iota_listen", args, paramTypes);

        }


        public static void SendMessageToRust(IntPtr walletHandle, string message, [MarshalAs(UnmanagedType.FunctionPtr)] MessageReceivedCallback callback, IntPtr context)
        {
            Type[] paramTypes = { typeof(IntPtr), typeof(string), typeof(MessageReceivedCallback), typeof(IntPtr) };
            Object[] args = { walletHandle, message, callback, context };

            DynamicPInvokeBuilder(typeof(void), ResolveLibraryNameFromPlatformType(), "iota_send_message", args, paramTypes);
        }

        public static byte EnableLogging(string filename, string filterLevel)
        {
            Type[] paramTypes = { typeof(string), typeof(string) };
            Object[] args = { filename, filterLevel };

            return (byte)DynamicPInvokeBuilder(typeof(byte), ResolveLibraryNameFromPlatformType(), "iota_init_logger", args, paramTypes);
        }

        public static void CloseIotaWallet(IntPtr walletHandle)
        {
            Type[] paramTypes = { typeof(IntPtr) };
            Object[] args = { walletHandle };
            DynamicPInvokeBuilder(typeof(void), ResolveLibraryNameFromPlatformType(), "iota_destroy", args, paramTypes);
        }

        public static object? DynamicPInvokeBuilder(Type returnType, string libraryName, string methodName, Object[] args, Type[] paramTypes)
        {
            AssemblyName assemblyName = new AssemblyName($"dyn1_{libraryName}");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule($"dyn2_{libraryName}");
            MethodBuilder methodBuilder = moduleBuilder.DefinePInvokeMethod(methodName, libraryName, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.PinvokeImpl, CallingConventions.Standard, returnType, paramTypes, CallingConvention.Cdecl, CharSet.Ansi);
            methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
            moduleBuilder.CreateGlobalFunctions();

            MethodInfo dynamicMethod = moduleBuilder.GetMethod(methodName)!;
            object? res = dynamicMethod.Invoke(null, args);
            return res;
        }

    }
}
