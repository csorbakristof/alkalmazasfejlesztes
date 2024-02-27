using SerializeExample.Data;
using SerializeExample.Serializers;
using System;
using System.Collections.Generic;
using System.IO;

namespace SerializeExample
{
    class Program
    {
        static Book singleBook;
        static List<Book> bookList;
        static void Main(string[] args)
        {
            CreateData();
            Console.WriteLine("System.Text.JSON");
            SystemJson json1 = new SystemJson();
            json1.SerializeOne(singleBook);
            Console.WriteLine();
            json1.SerializeList(bookList);
            Console.WriteLine();

            Console.WriteLine("XmlSerializer");
            SystemXml xml = new SystemXml();
            xml.SerializeOne(singleBook);
            Console.WriteLine();
            xml.SerializeList(bookList);
            Console.WriteLine();

            Console.WriteLine("Done with serialization");

            string singleBookStringJson = File.ReadAllText(@"TextFiles\SingleBookJson.txt");
            string bookListStringJson = File.ReadAllText(@"TextFiles\BookListJson.txt");
            string singleBookStringXml = File.ReadAllText(@"TextFiles\SingleBookXml.txt");
            string bookListStringXml = File.ReadAllText(@"TextFiles\BookListXml.txt");


            Console.WriteLine("System.Text.JSON");
            
            json1.DeserializeOneFromString(singleBookStringJson);
            Console.WriteLine();
            json1.DeserializeListFromString(bookListStringJson);
            Console.WriteLine();

            Console.WriteLine("XmlSerializer");
            xml.DeserializeOneFromString(singleBookStringXml);
            Console.WriteLine();
            xml.DeserializeListFromString(bookListStringXml);

            Console.WriteLine("Done with deserialization");
            Console.ReadLine();
        }

        private static void CreateData()
        {
            singleBook = new Book()
            {
                Author = "First Author",
                Title = "Title A",
                Year = 2010,
                Id = 0
            };
            Chapter ch1 = new Chapter()
            {
                Number = 1,
                Title = "First chapter"
            };
            Chapter ch2 = new Chapter()
            {
                Number = 2,
                Title = "Second chapter"
            };
            singleBook.Chapters.Add(ch1);
            singleBook.Chapters.Add(ch2);

            Book secondBook = new Book()
            {
                Author = "Second Author",
                Title = "The second book",
                Year = 2015,
                Id = 1
            };
            Chapter ch3 = new Chapter()
            {
                Number = 1,
                Title = "Other first chapter"
            };
            Chapter ch4 = new Chapter()
            {
                Number = 2,
                Title = "Other second chapter"
            };

            secondBook.Chapters.Add(ch3);
            secondBook.Chapters.Add(ch4);

            bookList = new List<Book>();
            bookList.Add(singleBook);
            bookList.Add(secondBook);
        }
    }
}
