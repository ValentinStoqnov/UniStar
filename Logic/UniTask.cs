using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UniTask
    {
        public string TaskName { get; set; }
        public DateTime DeadLine { get; set; }
        public string TaskColor { get; set; }
        public bool IsCompleted { get; set; }
        public string Status { get; set; }

        public UniTask(string taskName, DateTime deadLine, bool isCompleted)
        {
            TaskName = taskName;
            DeadLine = deadLine;
            IsCompleted = isCompleted;
        }
    }
}
