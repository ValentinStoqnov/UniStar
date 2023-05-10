using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class FileSystemHelper
    {
        public static string GetDataFolderPath() 
        {
            string dataFolderPath = AppDomain.CurrentDomain.BaseDirectory + "Data\\";
            return dataFolderPath;
        }
        public static string GetXmlFilePathByClass(UniClass uniClass) 
        {
            string dataFolderPathWithXmlFileName = FileSystemHelper.GetDataFolderPath() + $"{uniClass.ClassName}.xml";
            return dataFolderPathWithXmlFileName;
        }
    }
}
