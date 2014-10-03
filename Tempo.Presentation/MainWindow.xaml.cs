using MahApps.Metro.Controls;

namespace Tempo.Presentation
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.MainWindowViewModel();
        }
    }
}
