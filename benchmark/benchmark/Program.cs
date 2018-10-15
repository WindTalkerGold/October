using System;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            TupleAsKey a = new TupleAsKey();
            for(int i=0;i<10;i++)
                a.Run();

        }
    }
}
