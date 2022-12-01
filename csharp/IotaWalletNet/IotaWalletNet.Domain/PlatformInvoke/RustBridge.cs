using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace IotaWalletNet.Domain.PlatformInvoke
{
    public static class RustBridge
    {
        public static string ResolveLibraryNameFromPlatformType()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "iota_wallet.dll";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "libiota_wallet.so";
            else
                throw new NotSupportedException($"Only windows x64 and Linux x86_64 is supported");
        }

        public delegate void MessageReceivedCallback(string message, string errors, IntPtr context);

        //public const string LIBRARY_NAME = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? WINDOWS_LIBRARY_NAME : LINUX_LIBRARY_NAME;
        //public static string s = "sdf";
        //[DllImport("s", EntryPoint = "iota_initialize", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IntPtr InitializeIotaWallet(string managerOptions, [MarshalAs(UnmanagedType.LPStr)] StringBuilder errorBuffer, int errorBufferSize);

        //[DllImport("iota_wallet", EntryPoint = "iota_send_message", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void SendMessageToRust(IntPtr walletHandle, string message, [MarshalAs(UnmanagedType.FunctionPtr)] MessageReceivedCallback callback, IntPtr context);

        //[DllImport("iota_wallet", EntryPoint = "iota_destroy", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void CloseIotaWallet(IntPtr walletHandle);

        public static IntPtr InitializeIotaWallet(string managerOptions, [MarshalAs(UnmanagedType.LPStr)] StringBuilder errorBuffer, int errorBufferSize)
        {
            Type[] paramTypes = { typeof(string), typeof(StringBuilder), typeof(int) };
            Object[] args = { managerOptions, errorBuffer, errorBufferSize };

            return (IntPtr)DynamicPInvokeBuilder(typeof(IntPtr), ResolveLibraryNameFromPlatformType(), "iota_initialize", args, paramTypes);
        }


        public static  void SendMessageToRust(IntPtr walletHandle, string message, [MarshalAs(UnmanagedType.FunctionPtr)] MessageReceivedCallback callback, IntPtr context)
        {
            Type[] paramTypes = { typeof(IntPtr), typeof(string), typeof(MessageReceivedCallback), typeof(IntPtr) };
            Object[] args = { walletHandle, message, callback, context };

            DynamicPInvokeBuilder(typeof(void), ResolveLibraryNameFromPlatformType(), "iota_send_message", args, paramTypes);
        }

        public static void CloseIotaWallet(IntPtr walletHandle)
        {
            Type[] paramTypes = { typeof(IntPtr) };
            Object[] args = { walletHandle  };
            DynamicPInvokeBuilder(typeof(void), ResolveLibraryNameFromPlatformType(), "iota_destroy", args, paramTypes);
        }

        public static object? DynamicPInvokeBuilder(Type returnType, string libraryName, string methodName, Object[] args, Type[] paramTypes)
        {
            AssemblyName assemblyName = new AssemblyName($"dyn1_{ResolveLibraryNameFromPlatformType()}");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule($"dyn2_{ResolveLibraryNameFromPlatformType()}");
            MethodBuilder methodBuilder = moduleBuilder.DefinePInvokeMethod(methodName, libraryName, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.PinvokeImpl, CallingConventions.Standard, returnType, paramTypes, CallingConvention.Cdecl, CharSet.Ansi);
            methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
            moduleBuilder.CreateGlobalFunctions();

            MethodInfo dynamicMethod = moduleBuilder.GetMethod(methodName)!;
            object? res = dynamicMethod.Invoke(null, args);
            return res;
        }

    }
}
