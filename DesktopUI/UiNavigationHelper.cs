using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI
{
    static class UiNavigationHelper
    {
        public static void OpenTasksWindow(UniClass uniClass) 
        {
            TasksWindow tasksWindow = new TasksWindow(uniClass);
            tasksWindow.ShowDialog();
        }
        public static void OpenTaskCreateWindow(UniClass uniClass) 
        {
            TaskCreateOrEditWindow taskCreateOrEditWindow = new TaskCreateOrEditWindow(uniClass);
            taskCreateOrEditWindow.ShowDialog();
        }
        public static void OpenTaskEditWindow(UniClass uniClass) 
        { 
            TaskCreateOrEditWindow taskCreateOrEditWindow = new TaskCreateOrEditWindow(uniClass);
            taskCreateOrEditWindow.ShowDialog();
        }
        public static void OpenClassCreateWindow() 
        {
            ClassCreateOrEditWindow classCreateOrEditWindow = new ClassCreateOrEditWindow();
            classCreateOrEditWindow.ShowDialog();
        }
        public static void OpenClassEditWindow() 
        {
            ClassCreateOrEditWindow classCreateOrEditWindow = new ClassCreateOrEditWindow();
            classCreateOrEditWindow.ShowDialog();
        }

    }
}
