using FluentAssertions;

namespace Tempo.Main.Entities
{
    public class Song
    {
        public Song
        (
            string name,
            string uri
        )
        {
            name.Should().NotBeNullOrEmpty();
            name.Should().NotBeNullOrWhiteSpace();
            uri.Should().NotBeNullOrEmpty();
            uri.Should().NotBeNullOrWhiteSpace();

            Name = name;
            Uri  = uri;
        }
        public string Name { get; private set; }
        public string Uri  { get; private set; }
    }
}
