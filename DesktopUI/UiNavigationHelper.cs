using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI
{
    static class UiNavigationHelper
    {
        public static void OpenTasksWindow() 
        {
            TasksWindow tasksWindow = new TasksWindow();
            tasksWindow.ShowDialog();
        }
        public static void OpenTaskCreateWindow() 
        {
            TaskCreateOrEditWindow taskCreateOrEditWindow = new TaskCreateOrEditWindow();
            taskCreateOrEditWindow.ShowDialog();
        }
        public static void OpenTaskEditWindow() 
        { 
            TaskCreateOrEditWindow taskCreateOrEditWindow = new TaskCreateOrEditWindow();
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
