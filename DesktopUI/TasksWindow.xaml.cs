using Logic;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

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
            RefreshClassTaskData(uniClass);
            StartTaskTimeLeftTimer();
        }

        private void StartTaskTimeLeftTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            foreach (UniTask task in LvTasks.Items)
            {
                task.TimeLeft -= new TimeSpan(0, 0, 1);
            }
        }
        private void RefreshClassTaskData(UniClass uniClass)
        {
            LvTasks.ItemsSource = TasksLogic.GetClassTasksListByClass(uniClass);
            TbClassName.Text = TasksLogic.GetClassName(uniClass);
            TasksLogic.SetTasksStatusesForUI(LvTasks.ItemsSource as ObservableCollection<UniTask>);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.RefreshClassesList();
        }
        private void BtnCreateTask_Click(object sender, RoutedEventArgs e)
        {
            UiNavigationHelper.OpenTaskCreateWindow(_currentUniClass);
            RefreshClassTaskData(_currentUniClass);
        }
        private void BtnEditTask_Click(object sender, RoutedEventArgs e)
        {
            UniTask uniTask = LvTasks.SelectedItem as UniTask;
            UiNavigationHelper.OpenTaskEditWindow(_currentUniClass, uniTask);
            RefreshClassTaskData(_currentUniClass);
        }
        private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (LvTasks.SelectedItem == null) return;
            UniTask uniTask = LvTasks.SelectedItem as UniTask;
            var isDeleteAccepted = MessageBox.Show($"Are you sure you want do delete {uniTask.TaskName} ?", "Delete Task", MessageBoxButton.YesNo);
            if (isDeleteAccepted == MessageBoxResult.No) return;
            TasksLogic.DeleteTask(_currentUniClass, uniTask);
            RefreshClassTaskData(_currentUniClass);
        }
        private void BtnFinishedOrNot_Click(object sender, RoutedEventArgs e)
        {
            //Getting the UniTask
            Button btn = sender as Button;
            Grid stackPanel = btn.Parent as Grid;
            ContentPresenter contentPresenter = stackPanel.TemplatedParent as ContentPresenter;          //DOESNT WORK RIGHT NOW 
            Border border = contentPresenter.Parent as Border;
            ListViewItem listViewItem = border.TemplatedParent as ListViewItem;
            UniTask uniTask = listViewItem.Content as UniTask;

            TasksLogic.MarkTaskAsFinishedOrUnfinished(_currentUniClass, uniTask);

            RefreshClassTaskData(_currentUniClass);
        }

        private void BtnMinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void BtnMaximizeWindow_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
        private void BtnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LvTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LvTasks.SelectedItem == null)
            {
                BtnEditTask.IsEnabled = false;
                BtnDeleteTask.IsEnabled = false;
            }
            else
            {
                BtnEditTask.IsEnabled = true;
                BtnDeleteTask.IsEnabled = true;
            }
        }
    }
}
