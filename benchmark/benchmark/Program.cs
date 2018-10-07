using System;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentDict a = new ConcurrentDict();
            for(int i=0;i<10;i++)
                a.Run();

        }
    }
}
