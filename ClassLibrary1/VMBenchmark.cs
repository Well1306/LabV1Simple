using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace ClassLibrary1
{
    [Serializable]
    public class VMBenchmark
    {
        [DllImport("mkl_.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mkl_sin(int n, double[] a, double[] y, char m);
        

        [DllImport("mkl_.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mkl_cos(int n, double[] a, double[] y, char m);

        //[DllImport("mkl_.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void mkl_sincos(int n, double[] a, double[] y, double[] z, char m);

        ObservableCollection<VMTime> times = new ObservableCollection<VMTime>();
        ObservableCollection<VMAccuracy> acc = new ObservableCollection<VMAccuracy>();

        //public ObservableCollection<VMTime> Times { get { return times; } }

        //public ObservableCollection<VMAccuracy> Accuracy { get { return acc; } }
        public VMTime selectedTime;
        public ObservableCollection<VMTime> Times { get { return times; } }
        public VMTime SelectedTime
        {
            get { return selectedTime; }
            set
            {
                selectedTime = value;
                OnPropertyChanged(nameof(SelectedTime));
            }
        }

        public VMAccuracy selectedAcc;
        public ObservableCollection<VMAccuracy> Accuracy { get { return acc; } }
        public VMAccuracy SelectedAcc
        {
            get { return selectedAcc; }
            set
            {
                selectedAcc = value;
                OnPropertyChanged(nameof(SelectedAcc));
            }
        }
        public VMBenchmark()
        {
            times = new ObservableCollection<VMTime>();
            acc = new ObservableCollection<VMAccuracy>();
        }
        public bool AddVMTime(VMGrid grid)
        {
            try
            {
                int n = grid.n;
                double[] a = new double[n];
                double[] y_h = new double[n];
                double[] y_l = new double[n];
                double[] y_e = new double[n];
                if (n != 1)
                {
                    for (int i = 0; i < n; i++) a[i] = grid.first + grid.step * i;
                }
                else
                {
                    a[0] = (grid.finish - grid.first) / 2;
                }
                VMTime t = new();
                switch (grid.func)
                {
                    case VMf.vmdSin:
                        t = new(grid, a, ref y_h, ref y_l, ref y_e, mkl_sin);
                        times.Add(t);
                        break;
                    case VMf.vmdCos:
                        t = new(grid, a, ref y_h, ref y_l, ref y_e, mkl_cos);
                        times.Add(t);
                        break;
                    case VMf.vmdSinCos:
                        t = new(grid, a, ref y_h, ref y_l, ref y_e, mkl_sin);
                        times.Add(t);
                        t = new(grid, a, ref y_h, ref y_l, ref y_e, mkl_cos);
                        times.Add(t);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
            return true;
        }
        public bool AddVMAccuracy(VMGrid grid)
        {
            try
            {
                VMAccuracy ac = new(grid);
                int n = grid.n;
                double[] a = new double[n];
                double[] y_h = new double[n];
                double[] y_l = new double[n];
                double[] y_e = new double[n];
                if (n != 1)
                {
                    for (int i = 0; i < n; i++) a[i] = grid.first + (grid.step * i);
                }
                else
                {
                    a[0] = (grid.finish - grid.first) / 2;
                }
                VMTime t = new();
                switch (grid.func)
                {
                    case VMf.vmdSin:
                        t = new(grid, a, ref y_h, ref y_l, ref y_e, mkl_sin);
                        break;
                    case VMf.vmdCos:
                        t = new(grid, a, ref y_h, ref y_l, ref y_e, mkl_cos);
                        break;
                    case VMf.vmdSinCos:
                        t = new(grid, a, ref y_h, ref y_l, ref y_e, mkl_sin);
                        ac.SetMaxAbs(a, y_h, y_e, y_l);
                        acc.Add(ac);
                        t = new(grid, a, ref y_h, ref y_l, ref y_e, mkl_cos);
                        break;
                }
                ac.SetMaxAbs(a, y_h, y_e, y_l);
                acc.Add(ac);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
            return true;
        }
        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < acc.Count; i++)
            {
                res += $"Test №{i + 1}:\n";
                res += acc[i].ToString();
            }
            for (int i = 0; i < times.Count; i++)
            {
                res += $"Test №{i + 1}:\n";
                res += times[i].ToString();
            }
            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
