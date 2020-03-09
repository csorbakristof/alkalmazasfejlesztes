using SerializeExample.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SerializeExample.Serializers
{
    public class SystemXml
    {
        public void SerializeOne(Book book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, book);
            Console.WriteLine(writer.ToString());
            
        }
        public void SerializeList(List<Book> books)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, books);
            Console.WriteLine(writer.ToString());
        }
        public void DeserializeOneFromString(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            Book book = (Book)serializer.Deserialize(new StringReader(xml));
            Console.WriteLine(book);
        }
        public void DeserializeListFromString(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            List<Book> books = (List<Book>)serializer.Deserialize(new StringReader(xml));
            foreach (var item in books)
            {
                Console.WriteLine(item);
            }
        }
    }
}
