using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private CreateOrEditWindowsState createOrEditWindowsState;
        private UniClass currentUniClass;
        private UniTask currentUniTask;
        private bool isDateSelected = false;

        public TaskCreateOrEditWindow(UniClass uniClass)
        {
            currentUniClass = uniClass;
            InitializeComponent();
        }
        
        public TaskCreateOrEditWindow(UniClass uniClass, CreateOrEditWindowsState state) : this(uniClass)
        {
            TbTitle.Text = "Create a new task";
            createOrEditWindowsState = state;
        }

        public TaskCreateOrEditWindow(UniClass uniClass, CreateOrEditWindowsState state, UniTask uniTask) : this(uniClass)
        {
            TbTitle.Text = "Edit task";
            TbTaskName.Text = uniTask.TaskName;
            DatePicker.Text = uniTask.DeadLine.ToShortDateString();
            createOrEditWindowsState = state;
            currentUniTask = uniTask;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            switch (createOrEditWindowsState) 
            { 
                case CreateOrEditWindowsState.Create:
                    TasksLogic.CreateNewTask(currentUniClass, TbTaskName.Text, DatePicker.Text);
                    break;
                case CreateOrEditWindowsState.Edit:
                    TasksLogic.EditTask(currentUniClass, currentUniTask, TbTaskName.Text, DatePicker.Text);
                    break;
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            isDateSelected = true;
            CheckIfUserCanSaveTask();
        }

        private void TbTaskName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckIfUserCanSaveTask();
        }

        private void CheckIfUserCanSaveTask()
        {
            if (TbTaskName.Text != string.Empty && isDateSelected == true && DatePicker.Text != string.Empty)
            {
                BtnSave.IsEnabled = true;
            }
            else 
            {
                BtnSave.IsEnabled = false;
            }
        }
    }
}
