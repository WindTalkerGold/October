using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benchmark.Loader;

namespace Benchmark
{
    internal class TupleAsKey
    {
        private sealed class Foo : IEquatable<Foo>
        {
            public string Item1 { get; }
            public string Item2 { get; }

            public Foo(string a, string b)
            {
                Item1 = a;
                Item2 = b;
            }

            public bool Equals(Foo other)
            {
                if (other == null)
                    return false;

                return Item1 == other.Item1 && Item2 == other.Item2;
            }

            public override bool Equals(object other)
            {
                return Equals(other as Foo);
            }

            public override int GetHashCode()
            {
                return CombineHashCodes(Item1.GetHashCode(), Item2.GetHashCode());
            }

            internal static int CombineHashCodes(int h1, int h2)
            {
                return (((h1 << 5) + h1) ^ h2);
            }

        }

        public void Run()
        {
            string guid1 = Guid.NewGuid().ToString();
            string guid2 = Guid.NewGuid().ToString();
            Tuple<string, string> tp1 = Tuple.Create(guid1, guid2);
            Tuple<string, string> tp2 = Tuple.Create(guid1, guid2);
            
            Foo foo1 = new Foo(guid1, guid2);
            Foo foo2 = new Foo(guid1, guid2);

            int hashCode;

            bool equals;

            double hashCodeCostTp = new Runner(()=> hashCode = tp1.GetHashCode()).Execute(1000).Ticks;
            double hashCodeCostFoo = new Runner(() => hashCode = foo1.GetHashCode()).Execute(1000).Ticks;

            double equalsCostTp = new Runner(()=>equals = tp1.Equals(tp2)).Execute(1000).Ticks;
            double equalsCostFoo = new Runner(() => equals = foo1.Equals(foo2)).Execute(1000).Ticks;

            Console.WriteLine(hashCodeCostFoo+" "+hashCodeCostTp+" "+ hashCodeCostTp / hashCodeCostFoo);
            Console.WriteLine(equalsCostFoo + " " + equalsCostTp+" "+equalsCostTp/equalsCostFoo);

        }
    }
}
