namespace StorageLibrary
{
    /// <summary>
    /// This class represents a game
    /// </summary>
    /// <inheritdoc cref="IStorable"/>
    public class Game :IStorable
    {
        /// <summary>
        /// Game's name
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Game's genre
        /// </summary>
        public string Genre { get; set; }
        /// <summary>
        /// Year of publish
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Game's id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Number of Game available in the Store
        /// </summary>
        public int InStock { get; set; }

        /// <summary>
        /// Consturctor for the game with proper parameters
        /// </summary>
        /// <param name="id"> <see cref="Id"/></param>
        /// <param name="stock"><see cref="InStock"/></param>
        /// <param name="title"><see cref="Title"/></param>
        /// <param name="genre"><see cref="Genre"/></param>
        /// <param name="year"><see cref="Year"/></param>
        public Game(string id,int stock, string title, string genre, int year)
        {
            Title = title;
            Genre = genre;
            Year = year;
            Id = id;
            InStock = stock;
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Game()
        {

        }

        public override string ToString()
        {
            return Id + ": '" + Title + "'(" + Year+") - Available: "+InStock;
        }
    }
}
