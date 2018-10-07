using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Benchmark.Loader;

namespace Benchmark
{
    class IntegerAsKey
    {
        struct Foo
        {
            public int Item1 { get; }
            public int Item2 { get; }

            public override bool Equals(object obj)
            {
                Console.WriteLine("Using Foo.object equals");
                if (obj is Foo)
                {
                    Foo other = (Foo) obj;
                    return other.Item1 == Item1 && other.Item2 == Item2;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return ((Item1 ^ Item2) >> 5) ^ Item1;
            }

            public Foo(int a, int b)
            {
                Item1 = a;
                Item2 = b;
            }
        }

        struct Foo2 : IEquatable<Foo2>
        {
            public Foo2(int a, int b)
            {
                Item1 = a;
                Item2 = b;
            }

            public int Item1 { get; }
            public int Item2 { get; }

            public bool Equals(Foo2 other)
            {
                Console.WriteLine("Using Foo2 equals");
                return Item1 == other.Item1 && Item2 == other.Item2;
            }

            public override bool Equals(object obj)
            {
                Console.WriteLine("Using Foo2.object equals");
                if (obj is Foo)
                {
                    Foo other = (Foo)obj;
                    return other.Item1 == Item1 && other.Item2 == Item2;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return ((Item1 ^ Item2) >> 5) ^ Item1;
            }
        }


        private Dictionary<Foo, int> dict1;
        private Dictionary<Foo2, int> dict2;
        
        public void Run()
        {
            dict1 = new Dictionary<Foo, int>();
            dict2 = new Dictionary<Foo2, int>();

            for (int i = 0; i < 1000; i++)
            {
                dict1[new Foo(i, i*2)] = i;
                dict2[new Foo2(i, i * 2)] = i;
            }

            bool b1 = false;
            bool b2 = false;
            long cost1 = new Runner(() => b1 = dict1.ContainsKey(new Foo(500, 1000))).Execute(10).Ticks;
            long cost2 = new Runner(() => b2 = dict2.ContainsKey(new Foo2(500, 1000))).Execute(10).Ticks;

            Console.WriteLine(b1+" "+b2);
            Console.WriteLine(cost1+" "+cost2);
        }
    }
}
