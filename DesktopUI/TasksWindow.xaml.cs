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
    /// Interaction logic for TasksWindow.xaml
    /// </summary>
    public partial class TasksWindow : Window
    {
        public TasksWindow(UniClass uniClass)
        {
            InitializeComponent();
            GetClassData(uniClass);
        }

        private void GetClassData(UniClass uniClass) 
        {
            LvTasks.ItemsSource = TasksLogic.GetUniTasksOC(uniClass);
            LblClassName.Content = TasksLogic.GetClassName(uniClass);
        }
    }
}
