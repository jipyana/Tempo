using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

using GalaSoft.MvvmLight.Ioc;

namespace Tempo.Presentation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = new MainWindowViewModel();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var player = new Tempo.Infrastructure.Player();
            player.Play();
        }
    }
}
