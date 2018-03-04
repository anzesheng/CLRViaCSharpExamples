using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppDomainsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Marshalling();
        }

        private static void Marshalling()
        {
            // Get the AppDomain that the calling thread is currently executing in
            AppDomain defaultDomain = Thread.GetDomain();
            Console.WriteLine($"Default AppDomain's friendly name = {defaultDomain.FriendlyName}");

            String entryAssemblyName = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine($"Main assembly = {entryAssemblyName}");


            // Demo #1
            Console.WriteLine($"{Environment.NewLine}Demo #1");

            // Static method
            AppDomain ad2 = AppDomain.CreateDomain("AD #2", null, null);

            // Cause the method to transition from the current AppDomain into the new AppDomain
            // The CLR will marshal the object by reference across the AppDomain boundaries
            MarshalByRefType byRefObj = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(entryAssemblyName, "AppDomainsSample.MarshalByRefType");

            // The GetType lies to you, proxy object is actually not an instance of the original type.
            Console.WriteLine($"Type={byRefObj.GetType()}");

            // When a source AppDomain wants to send or return the reference of an object to a destination AppDomain,
            // the CLR defines a proxy type in the destination AppDomain's loader heap.
            // This proxy type is defined using the original type's metadata, but the instance fields are not identical
            // to that of the original data type. Internally, the proxy object used a GCHandle instance that refers to the real object.
            Console.WriteLine($"IsTransparentProxy={RemotingServices.IsTransparentProxy(byRefObj)}");

            // Use proxy to call method
            // transition  the calling thread from the default AppDomain to the new AppDomain
            // the thread uses the proxy object's GCHandle field to find the real object in the new AppDomain,
            // and then it uses the real object to call the real SomeMethod method.
            byRefObj.SomeMethod();

            AppDomain.Unload(ad2);

            try
            {
                byRefObj.SomeMethod();
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("Failed call.");
            }



            Console.Read();
        }
    }
}
