using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace laba33
{
    class VMBenchmark
    {
        [DllImport("dll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mkl_sin(int n, double[] a, double[] y, char m);
        public int n { get; set; }
        public List<VMTime> times { get; set; }
        public List<VMAccuracy> acc { get; set; }
        public VMBenchmark()
        {
            times = new List<VMTime>();
            acc = new List<VMAccuracy>();
            n = 0;
        }

        public bool Add(int n, double start = 0, double end = 1)
        {
            try
            {
                VMAccuracy ac = new(n, start, end);

                double[] a = new double[n];
                double[] y_h = new double[n];
                double[] y_l = new double[n];
                if (n != 1)
                {
                    double step = (end - start) / (n - 1);
                    for (int i = 0; i < n; i++) a[i] = start + step * i;
                }
                else
                {
                    a[0] = (end - start) / 2;
                }
                VMTime t = new(n, a, ref y_h, ref y_l, mkl_sin);

                ac.SetMaxAbs(a, y_h, y_l);

                acc.Add(ac);
                times.Add(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
            this.n += 1;
            return true;
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < n; i++)
            {
                res += $"Test №{i + 1}:\n";
                res += acc[i].ToString() + times[i].ToString();
            }
            return res;
        }
        public bool SaveAsText(string filename)
        {
            StreamWriter file = null;
            try
            {
                using (file = new StreamWriter(filename, true))
                {
                    file.WriteLine(ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
            finally
            {
                if (file != null) file.Close();
            }
        }
    }
}
