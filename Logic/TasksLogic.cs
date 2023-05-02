using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Logic
{
    public static class TasksLogic
    {
        private static string dataFolderPath = AppDomain.CurrentDomain.BaseDirectory + "Data\\";

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

        public static void CreateNewTask(UniClass uniClass,string taskName,string taskDeadline) 
        {
            string dataFolderPathWithFileName = dataFolderPath + $"{uniClass.ClassName}.xml";

            //Loads XML File
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(dataFolderPathWithFileName);

            //Gets All the Class Tasks
            XmlNodeList TasksNodeList = xmlDocument.DocumentElement.GetElementsByTagName("Tasks");

            //Create the new task
            XmlNode TasksNode = xmlDocument.SelectSingleNode("/Class/Tasks");
            XmlElement newTask = xmlDocument.CreateElement("Task");
            TasksNode.AppendChild(newTask);

            XmlElement newTaskName = xmlDocument.CreateElement("TaskName");
            XmlElement newTaskDeadLine = xmlDocument.CreateElement("DeadLine");
            XmlElement newTaskIsCompleted = xmlDocument.CreateElement("isCompleted");

            newTask.AppendChild(newTaskName);
            newTask.AppendChild(newTaskDeadLine);
            newTask.AppendChild(newTaskIsCompleted);

            newTaskName.InnerText = taskName;
            newTaskDeadLine.InnerText = taskDeadline;
            newTaskIsCompleted.InnerText = "False";
            
            xmlDocument.Save(dataFolderPathWithFileName);
        }
    }
}
