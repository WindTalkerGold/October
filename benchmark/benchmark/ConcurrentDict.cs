using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benchmark.Loader;

namespace Benchmark
{
    internal sealed class ConcurrentDict
    {
        public void Run()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            ConcurrentDictionary<string, string> conDict = new ConcurrentDictionary<string, string>();

            string keyToLookup1 = string.Empty, keyToLookup2;
            for (int i = 0; i < 100; i++)
            {
                string guid = Guid.NewGuid().ToString();
                dict.Add(guid, guid);
                // use the last key to do loopup
                keyToLookup1 = guid;
            }

            keyToLookup2 = Guid.NewGuid().ToString();

            long cost1 = new Runner(() => dict.ContainsKey(keyToLookup1)).Execute(1000).Ticks;
            long cost2 = new Runner(() => dict.ContainsKey(keyToLookup2)).Execute(1000).Ticks;
            long cost3 = new Runner(() => conDict.ContainsKey(keyToLookup1)).Execute(1000).Ticks;
            long cost4 = new Runner(() => conDict.ContainsKey(keyToLookup2)).Execute(1000).Ticks;

            long cost5 = new Runner(() => dict[keyToLookup1] = string.Empty).Execute(1000).Ticks;
            long cost6 = new Runner(() => conDict[keyToLookup1] = string.Empty).Execute(1000).Ticks;

            Console.WriteLine($"{cost1} {cost2} {cost3} {cost4} {cost5} {cost6}");

        }

        public void Locality()
        {
            using (StreamReader sr = new StreamReader(File.OpenRead("test.txt")))
            {
            }

            FileStream fs = File.OpenRead("test.txt");
            // ... a lot of other code
            using (StreamReader sr = new StreamReader(fs))
            {

            }
        }
    }
}
