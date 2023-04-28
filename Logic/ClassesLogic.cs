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
        private static string dataFolderPath = AppDomain.CurrentDomain.BaseDirectory + "Data\\";


        public static void CreateNewClass(string className)
        {
            Directory.CreateDirectory(dataFolderPath);
            
            string dataFolderPathWithName = dataFolderPath+$"{className}.xml";
            using (XmlWriter writer = XmlWriter.Create(dataFolderPathWithName)) 
            { 
                writer.WriteStartDocument();
                writer.WriteStartElement("Class");
                writer.WriteElementString("ClassName", className);
                writer.WriteElementString("ClassColor", "PlaceHolder for Color");
                writer.WriteStartElement("Tasks");
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
        public static ObservableCollection<UniClass>  GetUniClasses() 
        { 
            DirectoryInfo directoryInfo = new DirectoryInfo(dataFolderPath);
            ObservableCollection<UniClass> classesList = new ObservableCollection<UniClass>();
            foreach (FileInfo xmlFile in directoryInfo.GetFiles()) 
            {
                UniClass uniClass = new UniClass(GetClassName(xmlFile.FullName), "Color", true, GetClassTasksList(xmlFile.FullName));
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
            XmlNodeList NameNodeList = xmlDocument.DocumentElement.GetElementsByTagName("ClassName");
            string className = NameNodeList.Item(0).InnerText;
            
            return className;
        }
        private static List<UniTask> GetClassTasksList(string xmlFilePath) 
        { 
            List<UniTask> classesList = new List<UniTask>();

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
                    classesList.Add(uniTask);
                }
            }
            return classesList;
        }
    }
}
