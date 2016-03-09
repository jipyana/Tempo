using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace Tempo.Presentation
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            vm = new ViewModel.MainWindowViewModel();
            this.DataContext = vm;
        }

        private readonly ViewModel.MainWindowViewModel vm;

        private void PlaylistElement_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.PlayCommand.Execute(null);
        }
    }
}
