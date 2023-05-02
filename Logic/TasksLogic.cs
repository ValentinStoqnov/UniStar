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

        public static ObservableCollection<UniTask> GetClassTasksListByClass(UniClass uniClass)
        {
            string xmlFilePath = dataFolderPath + $"{uniClass.ClassName}.xml";
            return GetClassTasksListByFilePath(xmlFilePath);
        }

        public static ObservableCollection<UniTask> GetClassTasksListByFilePath(string xmlFilePath)
        {
            ObservableCollection<UniTask> taskList = new ObservableCollection<UniTask>();

            //Loads XML File
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);

            //Gets All the Class Tasks
            XmlNodeList ProductsNodeList = xmlDocument.DocumentElement.GetElementsByTagName("Tasks");
            if (ProductsNodeList.Item(0).ChildNodes.Count > 0)
            {
                for (int i = 0; i < ProductsNodeList.Item(0).ChildNodes.Count; i++)
                {
                    string taskName = ProductsNodeList.Item(0).ChildNodes.Item(i).ChildNodes.Item(0).InnerText;
                    DateTime deadLine = (Convert.ToDateTime(ProductsNodeList.Item(0).ChildNodes.Item(i).ChildNodes.Item(1).InnerText));
                    bool isCompleted = (Convert.ToBoolean(ProductsNodeList.Item(0).ChildNodes.Item(i).ChildNodes.Item(2).InnerText));
                    UniTask uniTask = new UniTask(taskName, deadLine, isCompleted);
                    taskList.Add(uniTask);
                }
            }
            return taskList;
        }

        public static string GetClassName(UniClass uniClass)
        {
            return uniClass.ClassName;
        }

        public static void CreateNewTask(UniClass uniClass, string taskName, string taskDeadline)
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

        public static Tuple<ObservableCollection<UniTask>, ObservableCollection<UniTask>, ObservableCollection<UniTask>> GetFinishedUnfinishedCloseToDeadLineObservableCollections
(UniClass uniClass)
        {
            var finishedTasks = new ObservableCollection<UniTask>();
            var unfinishedTasks = new ObservableCollection<UniTask>();
            var closeToDeadLineTasks = new ObservableCollection<UniTask>();

            foreach (UniTask task in GetClassTasksListByClass(uniClass))
            {
                if (task.IsCompleted)
                {
                    finishedTasks.Add(task);
                }
                else if (!task.IsCompleted) 
                {
                    var TimeDifference = task.DeadLine - DateTime.Now;
                    if (TimeDifference.Days < 5)
                    {
                        closeToDeadLineTasks.Add(task);
                    }
                    else 
                    { 
                        unfinishedTasks.Add(task);
                    } 
                }
            }
            return Tuple.Create(finishedTasks,unfinishedTasks,closeToDeadLineTasks);
        }
        public static string DetermineTaskStatus(UniTask uniTask) 
        {
            if (!uniTask.IsCompleted)
            {
                var TimeDifference = uniTask.DeadLine - DateTime.Now;
                if (TimeDifference.Days < 5)
                {
                    return "Close to deadline";
                }
                else
                {
                    return "Unfinished";
                }
            }
            else 
            {
                return "Finished";
            }
        }  
    }
}
