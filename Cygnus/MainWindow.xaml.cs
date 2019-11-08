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

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<CVoluntario> items = new List<CVoluntario>();
            DateTime dataNascimento = new DateTime(2000, 01, 01);
            items.Add(new CVoluntario() { Nome = "John Doe", DataNascimento = dataNascimento, Endereco = "Av. 1" });
            items.Add(new CVoluntario() { Nome = "Jane Doe", DataNascimento = dataNascimento, Endereco = "Av. 1" });
            lvDataBinding.ItemsSource = items;
        }
    }
}
