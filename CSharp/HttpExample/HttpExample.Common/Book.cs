using System;
using System.Collections.Generic;
using System.Text;

namespace HttpExample.Common
{
    //DTO
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int NumberOfPages { get; set; }
        public int Year { get; set; }
        public override string ToString()
        {
            return $"{Id}\t{Author} - {Title} ({Year})";
        }
    }
}
