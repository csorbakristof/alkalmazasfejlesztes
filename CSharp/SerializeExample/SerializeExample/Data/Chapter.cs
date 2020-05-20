using System.Text.Json.Serialization;

namespace SerializeExample.Data
{
    public class Chapter
    {
        //[JsonPropertyNameAttribute("cim")]
        public string Title { get; set; }
        //[JsonPropertyNameAttribute("szam")]
        public int Number { get; set; }
        public override string ToString()
        {
            return $"{Number}: {Title}";
        }
    }
}
