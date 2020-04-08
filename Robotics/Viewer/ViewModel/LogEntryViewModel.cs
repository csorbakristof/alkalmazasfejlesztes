using Windows.UI.Text;
using Windows.UI.Xaml.Media;

namespace Viewer.ViewModel
{
    public class LogEntryViewModel
    {
        public string Text { get; set; }
        public Brush Brush { get; set; }
        public FontStyle Style { get; set; }
    }
}
