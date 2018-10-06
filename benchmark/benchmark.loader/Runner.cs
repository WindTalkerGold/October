
namespace Benchmark.Loader
{
    using System;
    using System.Diagnostics;

    public class Runner
    {
        private readonly Action code;

        public Runner(Action code)
        {
            this.code = code;
        }

        public TimeSpan Execute(int timesToExecute)
        {
            if (timesToExecute <= 0)
            {
                throw new ArgumentException($"{nameof(timesToExecute)} must be a positive integer");
            }

            // a Run outside loop to execlude possible compiling cost when Run is invoked for the first time;
            code();

            Stopwatch st = Stopwatch.StartNew();
            for (int i = 0; i < timesToExecute; i++)
            {
                code();
            }
            st.Stop();

            return st.Elapsed;
        }
    }
}
