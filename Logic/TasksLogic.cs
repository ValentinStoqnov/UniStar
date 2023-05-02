using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class TasksLogic
    {
        public static ObservableCollection<UniTask> GetUniTasksOC(UniClass uniClass) 
        {
            List<UniTask> tasks = new List<UniTask>(uniClass.UniTasks);
            ObservableCollection<UniTask> tasksOC = new ObservableCollection<UniTask>(tasks);
            return tasksOC;
        }

        public static string GetClassName(UniClass uniClass) 
        {
            string uniClassName = uniClass.ClassName;
            return uniClassName;
        }
    }
}
