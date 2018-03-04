using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppDomainsSample
{
    [Serializable]
    public sealed class MarshalByValType
    {
        private DateTime m_createionTime = DateTime.Now;
        public MarshalByValType()
        {
            Console.WriteLine($"{this.GetType().ToString()} ctor running in {Thread.GetDomain().FriendlyName}, Created on {m_createionTime:D}");
        }

        public override string ToString()
        {
            return m_createionTime.ToLongDateString();
        }
    }
}
