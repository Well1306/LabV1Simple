using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ClassLibrary1;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace WpfApp1
{

    [Serializable]
    public class VMFunctions
    {
        public VMf func { get; set; }
        public string f { get; set; }
        public VMFunctions(VMf ff, string s)
        {
            func = ff; f = s;
        }
        public VMFunctions() { }
    }
    [Serializable]
    public class VMfToList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public VMFunctions selectedFunc;
        public ObservableCollection<VMFunctions> Funcs { get; set; }

        public VMfToList()
        {
            Funcs = new ObservableCollection<VMFunctions>
            {
                new VMFunctions { func = VMf.vmdSin, f = "Sin" },
                new VMFunctions { func = VMf.vmdCos, f = "Cos" },
                new VMFunctions { func = VMf.vmdSinCos, f = "SinCos" }
            };
        }
        public VMFunctions SelectedFunc
        {
            get { return selectedFunc; }
            set
            {
                selectedFunc = value;
                OnPropertyChanged(nameof(SelectedFunc));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        


    }
}
