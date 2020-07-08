using HttpExample.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpExample.Client
{
    public class ClientCalls
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> EchoStringAsync(string message)
        {
            var content = new StringContent(message);
            var result = await client.PostAsync(Utilities.EchoUri, content);
            string stringres = await result.Content.ReadAsStringAsync();
            return stringres;
        }

        public async Task<string> GetBooksStringAsync()
        {
            var result = await client.GetAsync(Utilities.BooksUri);
            string stringres = await result.Content.ReadAsStringAsync();
            return stringres;
        }

        public async Task<List<Book>> GetBooksListAsync()
        {
            string stringres = await this.GetBooksStringAsync();
            List<Book> result = JsonConvert.DeserializeObject<List<Book>>(stringres);
            return result;
        }

        public async Task<string> PostBookAsync(Book book)
        {
            string jsonString = JsonConvert.SerializeObject(book);
            StringContent content = new StringContent(jsonString);
            var result = await client.PostAsync(Utilities.BooksUri, content);
            string stringres = await result.Content.ReadAsStringAsync();
            return stringres;
        }
    }
}

