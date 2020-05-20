using Newtonsoft.Json;
using SerializeExample.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SerializeExample.Serializers
{
    public class NewtonsoftJson
    {
        public void SerializeOne(Book book)
        {
            string json = JsonConvert.SerializeObject(book);
            Console.WriteLine(json);
        }
        public void SerializeList(List<Book> books)
        {
            string json = JsonConvert.SerializeObject(books);
            Console.WriteLine(json);
        }
        public void DeserializeOneFromString(string json)
        {
            Book book = JsonConvert.DeserializeObject<Book>(json);
            Console.WriteLine(book);
        }
        public void DeserializeListFromString(string json)
        {
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(json);
            foreach (var item in books)
            {
                Console.WriteLine(item);
            }
        }
    }
}
