using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [Serializable]
    public class VMGrid : INotifyPropertyChanged
    {
        public int n;
        public double first;
        public double finish;
        public double step;
        public VMf func;

        public event PropertyChangedEventHandler PropertyChanged;
       
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void SetStep()
        {
            step = (finish - first) / (n - 1);
        }
        public VMGrid()
        {
            n = 100; first = 0; finish = 1;
            step = (finish - first) / (n - 1);
            func = VMf.vmdSin;
        }
        public VMGrid(int n1, double fir, double fin, VMf f)
        { 
            n = n1; first = fir; finish = fin;
            step = (finish - first) / (n - 1); func = f;
        }
        public override string ToString()
        {
            string f = "";
            switch (func)
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
            return $"[{first}, {finish}], {n}, {f}, step = {step}";
        }
    }
}
