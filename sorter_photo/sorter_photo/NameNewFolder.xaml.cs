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
using System.Windows.Shapes;

namespace sorter_photo
{
    /// <summary>
    /// Логика взаимодействия для NameNewFolder.xaml
    /// </summary>
    public partial class NameNewFolder : Window
    {
        public NameNewFolder()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        
        public string Name
        {
            get { return NameFolder.Text; }
        }
    }
}
