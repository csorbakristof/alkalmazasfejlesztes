using RobotBrain.LogEntry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Viewer.ViewModel
{
    public class LogViewModel : ILogEntryVisitor
    {
        public ObservableCollection<LogEntryViewModel> LogEntries { get; set; }
            = new ObservableCollection<LogEntryViewModel>();

        public LogViewModel()
        {
            AddLogEntry("Starting...");
        }

        public void Visit(CommandCompleteLogEntry logEntry)
        {
            AddLogEntry($"Command {logEntry.CommandType} complete", true);
        }

        public void Visit(GenericLogEntry logEntry)
        {
            AddLogEntry($"Generic entry: {logEntry.Message}");
        }

        public void Visit(TickLogEntry logEntry)
        {
            // Note: TickLogEntry is not taken into account here.
        }

        private readonly Brush ImportantColorBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private readonly Brush NormalColorBrush = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
        private void AddLogEntry(string text, bool isImportant = false)
        {
            LogEntries.Add(new LogEntryViewModel()
            {
                Text = text,
                Brush = isImportant ? ImportantColorBrush : NormalColorBrush,
                Style = Windows.UI.Text.FontStyle.Normal
            });
        }
    }
}
