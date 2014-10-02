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
            Name = name;
            Uri = uri;
        }
        public string Name { get; private set; }
        public string Uri  { get; private set; }
    }
}
