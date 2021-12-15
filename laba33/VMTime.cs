using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba33
{
    class VMTime
    {
        public int n { get; set; }
        public TimeSpan time_h { get; set; }
        public TimeSpan time_l { get; set; }

        public delegate void function(int n, double[] a, double[] y, char m);
        public VMTime(int n, double[] a, ref double[] y1, ref double[] y2, function f)
        {
            try
            {
                this.n = n;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                f(n, a, y1, 'H');
                stopWatch.Stop();
                this.time_h = stopWatch.Elapsed;
                stopWatch.Reset();
                stopWatch.Start();
                f(n, a, y2, 'L');
                stopWatch.Stop();
                this.time_l = stopWatch.Elapsed;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public override string ToString()
        {
            return $"\tTime VML_H: {time_h}\n\tTime VML_EP: {time_l}\n";
        }
    }
}
