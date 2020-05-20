using System;

namespace HttpExample.Common
{
    //uri kiszervezés
    public class Utilities
    {
        public static readonly string Baseuri = "http://localhost:8000/";
        public static readonly string Echo = "echo/";
        public static readonly string Books = "books/";
        public static readonly string BooksUri = string.Concat(Baseuri, Books);
        public static readonly string EchoUri = string.Concat(Baseuri, Echo);
    }
}
