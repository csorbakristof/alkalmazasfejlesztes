using System.Text.Json.Serialization;

namespace SerializeExample.Data
{
    public class Chapter
    {
        public string Title { get; set; }

        public int Number { get; set; }

        public override string ToString()
        {
            return $"{Number}: {Title}";
        }
    }
}
