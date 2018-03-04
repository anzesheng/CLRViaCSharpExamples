using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppDomainsSample
{
    public sealed class MarshalByRefType : MarshalByRefObject
    {
        public MarshalByRefType()
        {
            Console.WriteLine($"{this.GetType().ToString()} ctor running in {Thread.GetDomain().FriendlyName}");
        }

        public void SomeMethod()
        {
            Console.WriteLine($"SomeMethod(), executing in {Thread.GetDomain().FriendlyName}");
        }

        public MarshalByValType MethodWithReturn()
        {
            Console.WriteLine($"MethodWithReturn(), executing in {Thread.GetDomain().FriendlyName}");
            MarshalByValType t = new MarshalByValType();
            return t;
        }

        public NonMarshalableType MethodArgAndReturn(string callingDomainName)
        {
            Console.WriteLine($"MethodWithReturn(), calling from {callingDomainName} to {Thread.GetDomain().FriendlyName}");
            NonMarshalableType t = new NonMarshalableType();
            return t;
        }
    }
}
