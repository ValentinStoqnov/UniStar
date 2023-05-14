using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UniTask : BasePropertyChanged
    {
        public string TaskName { get; set; }
        public DateTime DeadLine { get; set; }
        public TimeSpan TimeLeft { get => TasksLogic.CalculateTimeLeft(this); set { OnPropertyChanged(); } }
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
