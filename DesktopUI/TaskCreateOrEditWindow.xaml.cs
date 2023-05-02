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
    /// Interaction logic for TaskCreateOrEditWindow.xaml
    /// </summary>
    public partial class TaskCreateOrEditWindow : Window
    {
        UniClass _currentUniClass;
        
        public TaskCreateOrEditWindow(UniClass uniClass)
        {
            _currentUniClass = uniClass;
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            TasksLogic.CreateNewTask(_currentUniClass, TbTaskName.Text, DatePicker.Text);
            this.Close();
        }
    }
}
