using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppDomainsSample
{
    [Serializable]
    public sealed class NonMarshalableType
    {
        public NonMarshalableType()
        {
            Console.WriteLine($"Executing in {Thread.GetDomain().FriendlyName}");
        }
    }
}
