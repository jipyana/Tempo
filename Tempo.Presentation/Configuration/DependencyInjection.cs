using GalaSoft.MvvmLight.Ioc;

using Tempo.Main.Model;
using Tempo.Main.Model.Impl;

namespace Tempo.Presentation.Configuration
{
    public abstract class DependencyInjection
    {
        public static void Register()
        {
            SimpleIoc.Default.Register<IPlaylist, DummyPlayList>();
            SimpleIoc.Default.Register<ISongsImporter, SongsImporter>();
        }
    }
}
