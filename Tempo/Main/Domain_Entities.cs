using System.Diagnostics.Contracts;

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
            Contract.Requires(!string.IsNullOrEmpty     (name));
            Contract.Requires(!string.IsNullOrWhiteSpace(name));
            Contract.Requires(!string.IsNullOrEmpty     (uri));
            Contract.Requires(!string.IsNullOrWhiteSpace(uri));

            Name = name;
            Uri  = uri;
        }
        public string Name { get; private set; }
        public string Uri  { get; private set; }
    }
}
