using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private UniClass _currentUniClass;

        public TasksWindow(UniClass uniClass)
        {
            InitializeComponent();
            _currentUniClass = uniClass;
            RefreshClassData(uniClass);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.RefreshClassesList();
        }
        private void RefreshClassData(UniClass uniClass)
        {
            LvTasks.ItemsSource = TasksLogic.GetClassTasksListByClass(uniClass);
            LblClassName.Content = TasksLogic.GetClassName(uniClass);
            TasksLogic.SetTasksStatusesForUI(LvTasks.ItemsSource as ObservableCollection<UniTask>);
        }
        private void BtnCreateTask_Click(object sender, RoutedEventArgs e)
        {
            UiNavigationHelper.OpenTaskCreateWindow(_currentUniClass);
            RefreshClassData(_currentUniClass);
        }
        private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (LvTasks.SelectedItem != null)
            {
                UniTask uniTask = LvTasks.SelectedItem as UniTask;
                TasksLogic.DeleteTask(_currentUniClass, uniTask);
                RefreshClassData(_currentUniClass);
            }
        }
    }
}
