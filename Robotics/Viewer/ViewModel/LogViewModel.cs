using RobotBrain.LogEntry;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;

namespace Viewer.ViewModel
{
    public class LogViewModel : ILogEntryVisitor
    {
        public ObservableCollection<LogEntryViewModel> LogEntries { get; set; }
            = new ObservableCollection<LogEntryViewModel>();

        public LogViewModel()
        {
            AddLogEntry("Starting...", true, false);
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

        public void Visit(RobotEventLogEntry logEntry)
        {
            AddLogEntry($"Robot event: {logEntry.EventName}", false, true);
        }

        private readonly Brush ImportantColorBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private readonly Brush RobotEventColorBrush = new SolidColorBrush(Windows.UI.Colors.Blue);
        private readonly Brush NormalColorBrush = new SolidColorBrush(Windows.UI.Colors.Gray);
        private void AddLogEntry(string text, bool isImportant = false, bool isRobotEvent=false)
        {
#pragma warning disable CS4014 // Call not awaited...
            Windows.ApplicationModel.Core.CoreApplication.
                MainView.CoreWindow.Dispatcher.
                RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    Brush brush = isRobotEvent ? RobotEventColorBrush : NormalColorBrush;
                    if (isImportant)
                        brush = ImportantColorBrush;
                    LogEntries.Add(new LogEntryViewModel()
                    {
                        Text = text,
                        Brush = brush,
                        Style = Windows.UI.Text.FontStyle.Normal
                    });
                });
#pragma warning restore CS4014
        }
    }
}
