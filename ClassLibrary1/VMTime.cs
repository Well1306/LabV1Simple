using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public delegate void function(int n, double[] a, double[] y, char m);

    [Serializable]
    public class VMTime
    {
        public VMGrid grid;
        public TimeSpan time_h; //VML_HA
        public TimeSpan time_l; //VML_LA
        public TimeSpan time_e; //VML_EP
        public double l_h;
        public double e_h;
        public string moreInfo;
        public VMTime()
        { 
            grid = null; 
        }

        public VMTime(VMGrid g, double[] a, ref double[] y1, ref double[] y2, ref double[] y3, function f) : this()
        {
            try
            {
                grid = g;
                int n = g.n;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                f(n, a, y1, 'H');
                stopWatch.Stop();
                time_h = stopWatch.Elapsed;
                stopWatch.Reset();
                stopWatch.Start();
                f(n, a, y2, 'L');
                stopWatch.Stop();
                time_l = stopWatch.Elapsed;
                stopWatch.Start();
                f(n, a, y3, 'E');
                stopWatch.Stop();
                time_e = stopWatch.Elapsed;
                l_h = time_l / time_h;
                e_h = time_e / time_h;
            }
            catch (Exception ex)
            {
                l_h = 11231321;
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public string FSTR
        {
            get
            {
                string f = "";
                switch (grid.func)
                {
                    case VMf.vmdCos:
                        f = "Cos ";
                        break;
                    case VMf.vmdSin:
                        f = "Sin ";
                        break;
                    case VMf.vmdSinCos:
                        f = "SinCos ";
                        break;
                }
                return $"Function: {f}\n[{grid.first}, {grid.finish}], {grid.n} nodes";
            }
        }
        public string STR 
        {
            get 
            {
                return $"Time VML_HA: {time_h}\nTime VML_EP: {time_e}\nTime VML_LA: {time_l}\n"; 
            } 
        }
        public override string ToString()
        {
            string f = "";
            if (grid == null) return "";
            switch (grid.func)
            {
                case VMf.vmdCos:
                    f = "Cos ";
                    break;
                case VMf.vmdSin:
                    f = "Sin ";
                    break;
                case VMf.vmdSinCos:
                    f = "SinCos ";
                    break;
            }
            return $"Function: {f}\n[{grid.first}, {grid.finish}], {grid.n} nodes\nTime VML_HA: {time_h}\nTime VML_EP: {time_e}\nTime VML_LA: {time_l}\n";
        }
        public string MoreInfo
        {
            get
            {
                if (grid == null) return "";
                moreInfo = $"Time VML_EP / Time VML_HA: {e_h}\nTime VML_LA / Time VML_HA: {l_h}\n";
                return moreInfo;
            }

            set
            {
                moreInfo = value;
            }
        }
    }
}
