using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SerializeExample.Data
{
    //TODO: kikommentezettek szemléltetésképp lehetnek: megadható hogy mi szerializálódjon ki -newtonsoft,
    //és hogy milyen névvel -system.text
    //deszerializáláskor nem olvassa be rendesen ha nem a saját attribútumaival lévőt adsz neki!

    //[JsonObject(MemberSerialization.OptIn)]
    public class Book
    {
        //[JsonPropertyNameAttribute("cim")]
        //[JsonProperty]
        public string Title { get; set; }
        //[JsonPropertyNameAttribute("nev")]
        //[JsonProperty]
        public string Author { get; set; }
        //[JsonPropertyNameAttribute("kiadas eve")]
        public int Year { get; set; }

        //[JsonPropertyNameAttribute("azonosito")]
        public int Id { get; set; }

        //[JsonPropertyNameAttribute("fejezetek")]
        //[JsonProperty]
        public List<Chapter> Chapters { get; set; }
        public Book()
        {
            Chapters = new List<Chapter>();
        }
        public override string ToString()
        {
            string result= $"{Id} - {Author}: {Title} ({Year})\n";
            foreach (var item in Chapters)
            {
                result = string.Concat(result, item.ToString(),"\n");
            }
            return result;
        }
    }
}
