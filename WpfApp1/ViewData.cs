using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using ClassLibrary1;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace WpfApp1
{
    [Serializable]
    public class ViewData : INotifyPropertyChanged
    {
        public VMBenchmark B = new();
        public VMBenchmark _B
        {
            get
            { return B; }
            set
            {
                B = value;
                OnPropertyChanged(nameof(_B));
            }
        }
        [XmlIgnore] public VMfToList A { get; set; }
        public bool changed = false;
        public string ch
        {
            get
            {
                if (changed) return "Collection changed";
                else return "Collection not changed";
            }
        }

        public ViewData()
        {
            A = new();
        }
        public bool AddVMTime(VMGrid grid)
        {
            OnPropertyChanged(nameof(AddVMTime));
            changed = B.AddVMTime(grid);
            OnPropertyChanged(nameof(ch));
            return changed;
        }
        public bool AddVMAccuracy(VMGrid grid)
        {
            OnPropertyChanged(nameof(AddVMAccuracy));
            changed = B.AddVMAccuracy(grid);
            OnPropertyChanged(nameof(ch));
            return changed;
        }
        public bool Save(string filename)
        {
            FileStream file = null;
            try
            {
                using (file = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ViewData));
                    xmlSerializer.Serialize(file, this);
                    changed = false;
                    OnPropertyChanged(nameof(ch));
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}");
                return false;
            }
            finally
            {
                if (file != null) file.Close();
            }
        }
        public static bool Load(string filename, ref ViewData v1)
        {
            FileStream file = null;
            try
            {
                using (file = new FileStream(filename, FileMode.Open))
                {
                    v1.B.Accuracy.Clear();
                    v1.B.Times.Clear();
                    v1.A.Funcs.Clear();
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ViewData));
                    v1 = xmlSerializer.Deserialize(file) as ViewData;
                    v1.changed = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                v1 = new();
                MessageBox.Show($"Exception: {ex.Message}");
                return false;
            }
            finally
            {
                if (file != null) file.Close();
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
