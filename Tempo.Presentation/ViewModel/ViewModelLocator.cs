/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Tempo.Presentation"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using Tempo.Main.Model;
using Tempo.Main.Model.Impl;

namespace Tempo.Presentation.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public static class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            Configuration.DependencyInjection.Register();

            SimpleIoc.Default.Register<MainWindowViewModel>();
        }

        private static Lazy<MainWindowViewModel> _main = new Lazy<MainWindowViewModel>(()=>{ return ServiceLocator.Current.GetInstance<MainWindowViewModel>(); });
        public static MainWindowViewModel Main
        {
            get { return _main.Value; }
        }
        
        public static void Cleanup()
        { 
        }
    }
}
