namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            DictLookupCost runner = new DictLookupCost();
            for (int i = 0; i < 10; i++)
            {
                runner.Run();
            }
        }
    }
}
