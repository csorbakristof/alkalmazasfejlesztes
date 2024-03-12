using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SerializeExample.Data
{
    public class Book
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int Year { get; set; }

        public int Id { get; set; }

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
