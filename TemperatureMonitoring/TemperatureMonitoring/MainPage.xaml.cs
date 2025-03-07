using System.Collections.ObjectModel;
using TemperatureMonitoring.Model;

namespace TemperatureMonitoring
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<TempHum> TempHumList { get; set; }
            = new ObservableCollection<TempHum>();

        public MainPage()
        {
            InitializeComponent();
            // Note: Do not forget to set the binding context!
            BindingContext = this;
        }

        private void LoadButton_Clicked(object sender, EventArgs e)
        {
            var loader = new DataLoader();
            TempHumList.Clear();
            var appDirectory = System.AppContext.BaseDirectory;
            // Note: do not take all data, that is 21K records
            foreach (var measurement in loader.LoadCsv(Path.Combine(appDirectory, @"Data/data.csv")).Take(200))
            {
                TempHumList.Add(measurement);
            }
        }
    }
}
