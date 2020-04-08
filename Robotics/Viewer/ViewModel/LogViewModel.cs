using RobotBrain.LogEntry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewer.ViewModel
{
    public class LogViewModel : ILogEntryVisitor
    {
        public ObservableCollection<LogEntryViewModel> LogEntries { get; set; }
            = new ObservableCollection<LogEntryViewModel>();

        public void Visit(CommandCompleteLogEntry logEntry)
        {
            throw new NotImplementedException();
        }

        public void Visit(GenericLogEntry logEntry)
        {
            throw new NotImplementedException();
        }
    }
}
