using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benchmark.Loader;

namespace Benchmark
{
    internal sealed class StringComparers
    {
        /*189 567
          162 552
          465 561
          447 549
          444 549
          441 546
          432 558
          432 549
          300 552
          165 546*/
        public void Run()
        {
            string guid = Guid.NewGuid().ToString();

            IEqualityComparer<string> comp1 = StringComparer.Ordinal;
            IEqualityComparer<string> comp2 = StringComparer.OrdinalIgnoreCase;

            int hashCode;

            long cost1 = new Runner(() => hashCode = comp1.GetHashCode(guid)).Execute(1000).Ticks;
            long cost2 = new Runner(() => hashCode = comp2.GetHashCode(guid)).Execute(1000).Ticks;

            Console.WriteLine(cost1+" "+cost2);
        }
    }
}
