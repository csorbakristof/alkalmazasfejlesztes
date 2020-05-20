using HttpExample.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpExample.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Send this string: \n");
            string str = Console.ReadLine();

            ClientCalls client = new ClientCalls();
            string result1 = await client.EchoStringAsync(str);
            Console.WriteLine($"Result:\n{result1}");

            Console.WriteLine("Get books (string)");
            string result2 = await client.GetBooksStringAsync();
            Console.WriteLine($"Result:\n{ result2}");

            Console.WriteLine("Post a book");

            Book b = new Book()
            {
                Title = "Title",
                Author = "Author A",
                NumberOfPages = 257,
                Year = 2015
            };
            string result3 = await client.PostBookAsync(b);
            Console.WriteLine($"Result:\n{result3}");

            Console.WriteLine("Get books (json)");
            var result4 = await client.GetBooksListAsync();

            foreach (var item in result4)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
