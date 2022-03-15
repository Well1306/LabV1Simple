using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassLibrary1
{
    [Serializable]
    public class VMAccuracy
    {
        public VMGrid grid;
        public double maxAbs;
        public double x = 0;
        public double y_h;
        public double y_l;
        public double y_e;
        public string moreInfo;
        public VMAccuracy()
        {
            grid = null;
        }
        public VMAccuracy(VMGrid g) 
        {
            grid = g;
        }
        public bool SetMaxAbs(double[] a, double[] yh, double[] ye, double[] yl)
        {
            try
            {
                double m = 0;
                int k = 0;
                for (int i = 0; i < grid.n; i++)
                {
                    double max = Math.Abs(yh[i] - ye[i]);
                    if (max >= m)
                    {
                        m = max; k = i;
                    }
                }
                maxAbs = m;
                x = a[k];
                y_h = yh[k];
                y_l = yl[k];
                y_e = ye[k];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
            return true;
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
                return $"VML_HA: {y_h}\nVML_EP: {y_e}\nVML_LA: {y_l}\n"; 
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
            return $"Function: {f}\n[{grid.first}, {grid.finish}], {grid.n} nodes\nVML_HA: {y_h}\nVML_EP: {y_e}\nVML_LA: {y_l}\n";
        }
        public string MoreInfo
        {
            get
            {
                if (grid == null)
                {
                    return "";
                }
                moreInfo = $"Max abs {maxAbs}\nis reached on the argument {x}\n";
                
                return moreInfo;
            }
            set
            {
                moreInfo = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
