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
            TaskCreateOrEditWindow taskCreateOrEditWindow = new TaskCreateOrEditWindow(uniClass,CreateOrEditWindowsState.Create);
            taskCreateOrEditWindow.ShowDialog();
        }
        public static void OpenTaskEditWindow(UniClass uniClass, UniTask uniTask) 
        { 
            TaskCreateOrEditWindow taskCreateOrEditWindow = new TaskCreateOrEditWindow(uniClass,CreateOrEditWindowsState.Edit,uniTask);
            taskCreateOrEditWindow.ShowDialog();
        }
        public static void OpenClassCreateWindow() 
        {
            ClassCreateOrEditWindow classCreateOrEditWindow = new ClassCreateOrEditWindow(CreateOrEditWindowsState.Create);
            classCreateOrEditWindow.ShowDialog();
        }
        public static void OpenClassEditWindow(UniClass uniClass) 
        {
            ClassCreateOrEditWindow classCreateOrEditWindow = new ClassCreateOrEditWindow(uniClass, CreateOrEditWindowsState.Edit);
            classCreateOrEditWindow.ShowDialog();
        }

    }
}
