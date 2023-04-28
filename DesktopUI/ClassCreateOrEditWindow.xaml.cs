using Logic;
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

namespace DesktopUI
{
    /// <summary>
    /// Interaction logic for ClassCreateOrEditWindow.xaml
    /// </summary>
    public partial class ClassCreateOrEditWindow : Window
    {
        public ClassCreateOrEditWindow()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            ClassesLogic.CreateNewClass(TbClassName.Text);
            this.Close();
        }
    }
}
