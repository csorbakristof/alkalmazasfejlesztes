using HttpExample.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpExample.Server
{
    class Program
    {
        private static List<Book> books;
        private static object lockObject;
        private static HttpListener listener;

        static async Task Main()
        {
            lockObject = new object();
            books = new List<Book>();
            listener = new HttpListener();

            listener.Prefixes.Add(Utilities.EchoUri);
            listener.Prefixes.Add(Utilities.BooksUri);

            listener.Start();
            Console.WriteLine($"Listening...");
            
            while (listener.IsListening)
            {
                var context = await listener.GetContextAsync();
                try
                {
                    await HandlerMethodAsync(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                
            }

            listener.Close();
            Console.WriteLine("Stopped listening");
            Console.ReadKey();
        }

        private static async Task HandlerMethodAsync(HttpListenerContext ctx)
        {
            HttpListenerRequest req = ctx.Request;
            HttpListenerResponse resp = ctx.Response;
            Console.WriteLine($"URL: {req.Url} \t{req.HttpMethod}");
            if (req.Url.ToString().Equals(Utilities.BooksUri))
            {
                if (req.HttpMethod.Equals("POST"))
                    await HandleBookPostAsync(req, resp);
                else
                    await HandleBookGet(req, resp);
            }
            else
                await HandleEchoAsync(req, resp);
        }

        private static async Task<string> GetStringContentAsync(HttpListenerRequest req)
        {
            string result = "";
            using (var bodyStream = req.InputStream)
            {
                var encoding = req.ContentEncoding;
                using (var streamReader = new StreamReader(bodyStream, encoding))
                {
                    result = await streamReader.ReadToEndAsync();
                }
            }
            return result;
        }

        private static async Task BuildResponse(HttpListenerResponse resp, Encoding encoding, string content)
        {
            resp.StatusCode = 200;
            byte[] buffer = encoding.GetBytes(content);
            resp.ContentLength64 = buffer.Length;

            await resp.OutputStream.WriteAsync(buffer);
            resp.OutputStream.Close();
        }

        private static async Task HandleEchoAsync(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string reqContent = await GetStringContentAsync(req);
            
            Console.WriteLine($"Echo content:\t {reqContent}");

            await BuildResponse(resp, req.ContentEncoding, $"Echo content:\t {reqContent}");
        }

        private static async Task HandleBookPostAsync(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string reqcontent = await GetStringContentAsync(req);

            JObject json = JObject.Parse(reqcontent);
            Book newbook = json.ToObject<Book>();
            int newid;

            lock (lockObject)
            {
                newid = books.Count;
                newbook.Id = newid;
                books.Add(newbook);
            }

            await BuildResponse(resp, req.ContentEncoding,
                $"New book id:\t {newid}");            
        }

        private static async Task HandleBookGet(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string jsonString = JsonConvert.SerializeObject(books);

            await BuildResponse(resp, req.ContentEncoding, jsonString);
        }
    }
}
