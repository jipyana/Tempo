using System.Windows;

using MessageBox = System.Windows.Forms.MessageBox;

namespace Tempo.Presentation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }
}
