﻿using System;
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
        public static ObservableCollection<UniTask> GetClassTasksListByClass(UniClass uniClass)
        {
            string xmlFilePath = FileSystemHelper.GetXmlFilePathByClass(uniClass);
            return GetClassTasksListByFilePath(xmlFilePath);
        }
        public static ObservableCollection<UniTask> GetClassTasksListByFilePath(string xmlFilePath)
        {
            ObservableCollection<UniTask> taskList = new ObservableCollection<UniTask>();

            //Loads XML File
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);

            //Gets All the Class Tasks
            XmlNode ProductsNodeList = xmlDocument.SelectSingleNode("/Class/Tasks");
            if (ProductsNodeList.ChildNodes.Count > 0)
            {
                for (int i = 0; i < ProductsNodeList.ChildNodes.Count; i++)
                {
                    string taskName = ProductsNodeList.ChildNodes.Item(i).ChildNodes.Item(0).InnerText;
                    DateTime deadLine = (Convert.ToDateTime(ProductsNodeList.ChildNodes.Item(i).ChildNodes.Item(1).InnerText));
                    bool isCompleted = (Convert.ToBoolean(ProductsNodeList.ChildNodes.Item(i).ChildNodes.Item(2).InnerText));
                    UniTask uniTask = new UniTask(taskName, deadLine, isCompleted);
                    taskList.Add(uniTask);
                }
            }
            return taskList;
        }
        public static Tuple<ObservableCollection<UniTask>, ObservableCollection<UniTask>, ObservableCollection<UniTask>> GetTaskStatusesObservableCollections(UniClass uniClass)
        {
            var finishedTasks = new ObservableCollection<UniTask>();
            var unfinishedTasks = new ObservableCollection<UniTask>();
            var closeToDeadLineTasks = new ObservableCollection<UniTask>();

            foreach (UniTask task in GetClassTasksListByClass(uniClass))
            {
                switch (TasksLogic.DetermineTaskStatus(task))
                {
                    case TaskStatuses.Finished:
                        finishedTasks.Add(task);
                        break;
                    case TaskStatuses.Unfinished:
                        unfinishedTasks.Add(task);
                        break;
                    case TaskStatuses.CloseToDeadline:
                        closeToDeadLineTasks.Add(task);
                        break;
                }
            }
            return Tuple.Create(finishedTasks, unfinishedTasks, closeToDeadLineTasks);
        }

        public static string GetClassName(UniClass uniClass)
        {
            return uniClass.ClassName;
        }
        public static Enum DetermineTaskStatus(UniTask uniTask)
        {
            if (!uniTask.IsCompleted)
            {
                var TimeDifference = uniTask.DeadLine - DateTime.Now;
                if (TimeDifference.Days < 5)
                {
                    return TaskStatuses.CloseToDeadline;
                }
                else
                {
                    return TaskStatuses.Unfinished;
                }
            }
            else
            {
                return TaskStatuses.Finished;
            }
        }
        public static void SetTasksStatusesForUI(ObservableCollection<UniTask> TasksList)
        {
            foreach (UniTask task in TasksList)
            {
                switch (TasksLogic.DetermineTaskStatus(task))
                {
                    case TaskStatuses.Finished:
                        task.Status = "Finished";
                        break;
                    case TaskStatuses.Unfinished:
                        task.Status = "Unfinished";
                        break;
                    case TaskStatuses.CloseToDeadline:
                        task.Status = "Close to deadline";
                        break;
                }
            }
        }
        public static void CreateNewTask(UniClass uniClass, string taskName, string taskDeadline)
        {
            //Loads XML File
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(FileSystemHelper.GetXmlFilePathByClass(uniClass));

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

            xmlDocument.Save(FileSystemHelper.GetXmlFilePathByClass(uniClass));
        }
        public static void DeleteTask(UniClass uniClass, UniTask uniTask)
        {
            //Loads the Xml Document
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(FileSystemHelper.GetXmlFilePathByClass(uniClass));
            //Deletes the selected Task
            XmlNode TasksNode = xmlDocument.SelectSingleNode($"/Class/Tasks");
            XmlNode SingleTaskNode = xmlDocument.SelectSingleNode($"/Class/Tasks/Task[TaskName='{uniTask.TaskName}' and DeadLine='{uniTask.DeadLine.Date.ToShortDateString()}']");
            TasksNode.RemoveChild(SingleTaskNode);
            xmlDocument.Save(FileSystemHelper.GetXmlFilePathByClass(uniClass));
        }  
    }
}
