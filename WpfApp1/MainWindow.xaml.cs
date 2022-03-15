using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary1;
using System.Runtime;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewData VD = null;
        VMGrid grid = null;
        public MainWindow()
        {
            InitializeComponent();
            VMfToList a = new();
            addTime.IsEnabled = false;
            addAcc.IsEnabled = false;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (VD.changed == true)
            {
                MessageBoxResult res = MessageBox.Show("Сохранить файл?", "Изменения не сохранены", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes) MenuItem_Save(sender, (RoutedEventArgs) e);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grid = new();
            VD = (ViewData) DataContext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grid.func = VD.A.selectedFunc.func;
            MessageBox.Show(VD.B.Times.Count.ToString());
        }

        private void TextBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true; // отклоняем ввод
            }
        }
        private void TextBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && e.Text != ",")
            {
                e.Handled = true; // отклоняем ввод
            }
        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((TextBox1.Text.Length == 0) || (TextBox2.Text.Length == 0) || (TextBox3.Text.Length == 0))
            {
                addTime.IsEnabled = false;
                addAcc.IsEnabled = false;
            }
            else
            {
                addTime.IsEnabled = true;
                addAcc.IsEnabled = true;
            }
           
            try
            {
                grid.n = Convert.ToInt32(TextBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((TextBox1.Text.Length == 0) || (TextBox2.Text.Length == 0) || (TextBox3.Text.Length == 0))
            {
                addTime.IsEnabled = false;
                addAcc.IsEnabled = false;
            }
            else
            {
                addTime.IsEnabled = true;
                addAcc.IsEnabled = true;
            }
            try
            { 
               grid.first = Convert.ToDouble(TextBox2.Text);
                grid.SetStep();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((TextBox1.Text.Length == 0) || (TextBox2.Text.Length == 0) || (TextBox3.Text.Length == 0))
            {
                addTime.IsEnabled = false;
                addAcc.IsEnabled = false;
            }
            else
            {
                addTime.IsEnabled = true;
                addAcc.IsEnabled = true;
            }
            try
            {
               grid.finish = Convert.ToDouble(TextBox3.Text);
                grid.SetStep();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addTime_Click(object sender, RoutedEventArgs e)
        {
            if (VD.A.selectedFunc == null) MessageBox.Show("Выберите функцию");
            else
            {
                grid.func = VD.A.selectedFunc.func;
                VD.AddVMTime(grid);
            }
        }

        private void addAcc_Click(object sender, RoutedEventArgs e)
        {
            if (VD.A.selectedFunc == null) MessageBox.Show("Выберите функцию");
            else
            {
                grid.func = VD.A.selectedFunc.func;
                VD.AddVMAccuracy(grid);
                //MessageBox.Show(grid.ToString());
            }
        }

        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            try
            {
                if(VD.changed== true)
                {
                    MessageBoxResult res = MessageBox.Show("Сохранить файл?", "Изменения не сохранены", MessageBoxButton.YesNoCancel);
                    if (res == MessageBoxResult.Yes) MenuItem_Save(sender, e);
                    else if (res == MessageBoxResult.Cancel) return;
                }
                VD._B.SelectedTime.MoreInfo = "";
                VD.B.Times.Clear();
                VD._B.SelectedAcc.MoreInfo = "";
                VD.B.Accuracy.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            if (VD.changed == true)
            {
                MessageBoxResult res = MessageBox.Show("Сохранить файл?", "Изменения не сохранены", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Yes) MenuItem_Save(sender, e);
                else if (res == MessageBoxResult.Cancel) return;
            }
            Microsoft.Win32.OpenFileDialog file = new();
            if (file.ShowDialog() == true)
            {
                ViewData.Load(file.FileName, ref VD);
                DataContext = null;
                DataContext = VD;
            }
        }
        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog file = new();
            if (file.ShowDialog() == true)
                VD.Save(file.FileName);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
