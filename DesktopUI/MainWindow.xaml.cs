﻿using Logic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<UniClass> uniClassesCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            RefreshClassesList();
            //test();
        }

        void test()
        {
            //TODO:
            //callendar
            //Possibly disabled buttons saying why they are disabled

            var TimeDifference = DateTimeOffset.Now - uniClassesCollection[2].UniTasks[1].DeadLine;
            MessageBox.Show(TimeDifference.Days.ToString());

        }

        private void BtnCreateClass_Click(object sender, RoutedEventArgs e)
        {
            UiNavigationHelper.OpenClassCreateWindow();
            RefreshClassesList();
        }
        private void BtnEditClass_Click(object sender, RoutedEventArgs e)
        {
            if (LvClasses.SelectedItem == null) return;
            UniClass uniClass = LvClasses.SelectedItem as UniClass;
            UiNavigationHelper.OpenClassEditWindow(uniClass);
            RefreshClassesList();
        }
        private void BtnDeleteClass_Click(object sender, RoutedEventArgs e)
        {
            if (LvClasses.SelectedItem == null) return;
            UniClass uniClass = LvClasses.SelectedItem as UniClass;
            var isDeleteAccepted = MessageBox.Show($"Are you sure you want do delete {uniClass.ClassName} ?", "Delete Class",MessageBoxButton.YesNo);
            if (isDeleteAccepted == MessageBoxResult.No) return;
            ClassesLogic.DeleteClass(uniClass);
            RefreshClassesList();

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
            Application.Current.Shutdown();
        }

        private void LvClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LvClasses.SelectedItem == null)
            {
                BtnEditClass.IsEnabled = false;
                BtnDeleteClass.IsEnabled = false;
            }
            else
            {
                BtnEditClass.IsEnabled = true;
                BtnDeleteClass.IsEnabled = true;
            }
        }
        private void LvClasses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LvClasses.SelectedItem == null) return;
            ListView classesListView = sender as ListView;
            UniClass selectedUniClass = classesListView.SelectedItem as UniClass;
            UiNavigationHelper.OpenTasksWindow(selectedUniClass);
        }

        public void RefreshClassesList()
        {
            uniClassesCollection = ClassesLogic.GetUniClassesFullData();
            LvClasses.ItemsSource = uniClassesCollection;
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (LvClasses.SelectedItem == null) return;
            UniClass? selectedUniClass = LvClasses.SelectedItem as UniClass;
            UiNavigationHelper.OpenTasksWindow(selectedUniClass);
        }
    }
}
