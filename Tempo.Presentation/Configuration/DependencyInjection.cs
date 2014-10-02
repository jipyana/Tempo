using GalaSoft.MvvmLight.Ioc;

using Tempo.Main.Model;
using Tempo.Main.Model.Impl;
using Tempo.Infrastructure.AudioPlayer;

namespace Tempo.Presentation.Configuration
{
    public abstract class DependencyInjection
    {
        public static void Register()
        {
            SimpleIoc.Default.Register<IPlaylist, DummyPlayList>();
            SimpleIoc.Default.Register<ISongsImporter, SongsImporter>();

            SimpleIoc.Default.Register<IAudioPlayer, AudioPlayer>();
        }
    }
}
