using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class UniClass
    {
        public string ClassName { get; set; }
        public List<UniTask> Tasks { get; set; }
        public bool IsCompleted { get; set; }

        public UniClass(string className, List<UniTask> tasks, bool isCompleted)
        {
            ClassName = className;
            Tasks = tasks;  
            IsCompleted = isCompleted;
        }
    }
}
