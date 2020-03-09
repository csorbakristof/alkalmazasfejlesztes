namespace StorageLibrary
{
    /// <summary>
    /// This class is a common interface for items to collect in the Store
    /// </summary>
    public interface IStorable
    {
        /// <summary>
        /// Item's Id
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// Number of this item available in the Store
        /// </summary>
        int InStock { get; set; }
    }
}
