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
        private CreateOrEditWindowsState createOrEditWindowsState;
        private UniClass currentUniClass;
        private UniTask currentUniTask;

        public TaskCreateOrEditWindow(UniClass uniClass)
        {
            currentUniClass = uniClass;
            InitializeComponent();
        }
        
        public TaskCreateOrEditWindow(UniClass uniClass, CreateOrEditWindowsState state) : this(uniClass)
        {
            createOrEditWindowsState = state;
        }

        public TaskCreateOrEditWindow(UniClass uniClass, CreateOrEditWindowsState state, UniTask uniTask) : this(uniClass)
        {
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
    }
}
