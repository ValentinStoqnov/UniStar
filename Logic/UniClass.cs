using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UniClass
    {
        public string ClassName { get; set; }
        public string ClassColor { get; set; }
        public bool IsCompleted { get; set; }
        public List<UniTask> UniTasks { get; set; }

        public UniClass(string className, string classColor , bool isCompleted, List<UniTask> uniTasks)
        {
            ClassName = className;
            ClassColor = classColor;
            IsCompleted = isCompleted;
            UniTasks = uniTasks;
        }
    }
}
