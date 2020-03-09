namespace StorageLibrary
{
    /// <summary>
    /// This class represents a book
    /// </summary>
    /// <inheritdoc cref="IStorable"/>
    public class Book :IStorable
    {
        /// <summary>
        /// Book's name
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Book's author
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Book's id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Number of available Book in the Store
        /// </summary>
        public int InStock { get; set; }

        /// <summary>
        /// Constructor for the book with proper parameters
        /// </summary>
        /// <param name="id"><see cref="Id"/></param>
        /// <param name="stock"><see cref="InStock"/></param>
        /// <param name="title"><see cref="Title"/></param>
        /// <param name="author"><see cref="Author"/></param>
        public Book(string id,int stock, string title, string author)
        {
            Title = title;
            Author = author;
            Id = id;
            InStock = stock;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Book()
        {

        }

        public override string ToString()
        {
            return Id + ": '" + Title + "' by " + Author + " - Available: " + InStock;
        }
    }
}
