using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba33
{
    class VMAccuracy
    {
        public double start { get; set; }
        public double end { get; set; }
        public int n { get; set; }
        public double maxAbs { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public VMAccuracy(int n, double start, double end)
        {
            this.start = start; this.end = end; this.n = n;
        }
        public void SetMaxAbs(double[] a, double[] y_h, double[] y_l)
        {
            try
            {
                double m = 0;
                int k = 0;
                for (int i = 0; i < n; i++)
                {
                    double max = Math.Abs(y_h[i] - y_l[i]);
                    if (max >= m)
                    {
                        m = max; k = i;
                    }
                }
                this.maxAbs = m;
                this.x = a[k];
                this.y = Math.Sin(this.x);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public override string ToString()
        {
            return $"\t[{start}, {end}], count {n}, max abs {maxAbs}, sin({x}) = {y}\n";
        }
    }
}
