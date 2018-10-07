using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    public sealed class DoubleAsKey
    {
        private Dictionary<double, double> dict;

        public void Run()
        {
            dict = new Dictionary<double, double>();
            double oneThird = 1d / 3d;
            dict.Add(oneThird, oneThird);

            double oneThird2 = double.Parse(oneThird.ToString());

            Console.WriteLine(dict.ContainsKey(oneThird));
            Console.WriteLine(dict.ContainsKey(oneThird2));
        }
    }
}
