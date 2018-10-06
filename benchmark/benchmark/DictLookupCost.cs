
using System;
using System.Collections.Generic;
using Benchmark.Loader;

namespace Benchmark
{
    /**
     * Execute Run result with Release mode:
     *  Three lookup cost: 958
        One lookup cost: 375
        Three lookup cost: 1090
        One lookup cost: 327
        Three lookup cost: 1111
        One lookup cost: 330
        Three lookup cost: 943
        One lookup cost: 327
        Three lookup cost: 943
        One lookup cost: 327
        Three lookup cost: 943
        One lookup cost: 327
        Three lookup cost: 1234
        One lookup cost: 327
        Three lookup cost: 955
        One lookup cost: 324
        Three lookup cost: 913
        One lookup cost: 321
        Three lookup cost: 1213
        One lookup cost: 318      */
    internal sealed class DictLookupCost
    {
        private class Foo
        {
            public int Bar { get; set; }
        }

        private Dictionary<string, Foo> dict;
        private string keyToLookup;

        private long cost1, cost2;

        public void Run()
        {
            PrepareContext();

            Execute();

            DisplayResult();
        }

        private void PrepareContext()
        {
            dict = new Dictionary<string, Foo>();
            for (int i = 0; i < 100; i++)
            {
                string guid = Guid.NewGuid().ToString();
                dict.Add(guid, new Foo());
                // use the last key to do loopup
                keyToLookup = guid;
            }
        }

        private void Execute()
        {
            cost1 = new Runner(ThreeLoopup).Execute(1000).Ticks;
            cost2 = new Runner(OneLoopup).Execute(1000).Ticks;
        }

        private void DisplayResult()
        {
            Console.WriteLine("Three lookup cost: " + cost1);
            Console.WriteLine("One lookup cost: " + cost2);
        }

        private void ThreeLoopup()
        {
            dict[keyToLookup].Bar++;
            dict[keyToLookup].Bar++;
            dict[keyToLookup].Bar++;
        }

        private void OneLoopup()
        {
            Foo foo = dict[keyToLookup];
            foo.Bar++;
            foo.Bar++;
            foo.Bar++;
        }

    }
}
