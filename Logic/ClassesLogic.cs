using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Logic
{
    public static class ClassesLogic
    {
        public static void CreateNewClass(string className, string classColor)
        {
            Directory.CreateDirectory(FileSystemHelper.GetDataFolderPath());

            string dataFolderPathWithName = FileSystemHelper.GetDataFolderPath() + $"{className}.xml";
            using (XmlWriter writer = XmlWriter.Create(dataFolderPathWithName))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Class");
                writer.WriteElementString("ClassName", className);
                writer.WriteElementString("ClassColor", classColor);
                writer.WriteStartElement("Tasks");
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
        public static void DeleteClass(UniClass uniClass)
        {
            File.Delete(FileSystemHelper.GetXmlFilePathByClass(uniClass));
        }
        public static void EditClass(UniClass uniClass, string newClassName, string newClassColor)
        {
            //Loads XML File
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(FileSystemHelper.GetXmlFilePathByClass(uniClass));

            //Sets the new color
            XmlNode ColorNode = xmlDocument.SelectSingleNode("/Class/ClassColor");
            ColorNode.InnerText = newClassColor;

            //Sets the new name
            XmlNode NameNode = xmlDocument.SelectSingleNode("/Class/ClassName");
            NameNode.InnerText = newClassName;

            //Saves the document
            if (uniClass.ClassName != newClassName)
            {
                xmlDocument.Save(FileSystemHelper.GetDataFolderPath() + $"{newClassName}.xml");
                File.Delete(FileSystemHelper.GetXmlFilePathByClass(uniClass));
            }
            else 
            {
                xmlDocument.Save(FileSystemHelper.GetXmlFilePathByClass(uniClass));
            }
        }
        public static ObservableCollection<UniClass> GetUniClasses()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(FileSystemHelper.GetDataFolderPath());
            ObservableCollection<UniClass> classesList = new ObservableCollection<UniClass>();
            foreach (FileInfo xmlFile in directoryInfo.GetFiles())
            {
                UniClass uniClass = new UniClass(GetClassName(xmlFile.FullName), GetClassColor(xmlFile.FullName), true, TasksLogic.GetClassTasksListByFilePath(xmlFile.FullName));
                classesList.Add(uniClass);
            }
            return classesList;
        }
        private static string GetClassName(string xmlFilePath)
        {
            //Loads XML File
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);

            //Gets the Name
            XmlNode NameNode = xmlDocument.SelectSingleNode("/Class/ClassName");
            string className = NameNode.InnerText;

            return className;
        }
        private static string GetClassColor(string xmlFilePath)
        {
            //Loads XML File
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFilePath);

            //Gets the Name
            XmlNode ColorNode = xmlDocument.SelectSingleNode("/Class/ClassColor");
            string classColor = ColorNode.InnerText;

            return classColor;
        }
        public static void SetClassTasksByStausForUI(ObservableCollection<UniClass> uniClasses)
        {
            foreach (UniClass uniClass in uniClasses)
            {
                var Filter = TasksLogic.GetTaskStatusesObservableCollections(uniClass);
                uniClass.FinishedTasks = Filter.Item1.Count;
                uniClass.UnfinishedTasks = Filter.Item2.Count;
                uniClass.CloseToDeadlineTasks = Filter.Item3.Count;
            }
        }
    }
}

