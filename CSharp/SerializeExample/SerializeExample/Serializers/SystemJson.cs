using SerializeExample.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace SerializeExample.Serializers
{
    public class SystemJson
    {
        public void SerializeOne(Book book)
        {
            string json = JsonSerializer.Serialize(book);
            Console.WriteLine(json);
        }
        public void SerializeList(List<Book> books)
        {
            string json = JsonSerializer.Serialize(books);
            Console.WriteLine(json);
        }
        public void DeserializeOneFromString(string json)
        {
            Book book = JsonSerializer.Deserialize<Book>(json);
            Console.WriteLine(book);
        }
        public void DeserializeListFromString(string json)
        {
            List<Book> books = JsonSerializer.Deserialize<List<Book>>(json);
            foreach (var item in books)
            {
                Console.WriteLine(item);
            }
        }
    }
}
