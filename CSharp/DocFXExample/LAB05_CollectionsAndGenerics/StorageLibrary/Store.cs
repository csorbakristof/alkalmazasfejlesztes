using System;
using System.Collections.Generic;

namespace StorageLibrary
{
    /// <summary>
    /// This class represents a class which collect <typeparamref name="T"/>  items
    /// </summary>
    /// <typeparam name="T">The element type of the items in the Store</typeparam>
    public class Store<T> where T : IStorable
    {
        private Dictionary<string, IStorable> storage = new Dictionary<string, IStorable>();

        /// <summary>
        /// Number of IStorables in the Store
        /// </summary>
        /// <returns>Number of IStorables in the Store</returns>
        public int Count()
        {
            return storage.Count;
        }

        /// <summary>
        /// Adds an item to the Store
        /// </summary>
        /// <param name="item">Item to add to the Store. Item's type must be <typeparamref name="T"/></param>
        public void Insert(IStorable item)
        {
            if (item is null || string.Equals(item.Id, null)) throw new ArgumentNullException();
            if (storage.ContainsKey(item.Id)) return;
            if (item.InStock <= 0) return;
            storage.Add(item.Id, item);
        }

        /// <summary>
        /// Adds every item in items to the Store
        /// </summary>
        /// <param name="items"></param>
        public void InsertMany(List<IStorable> items)
        {
            foreach (var item in items)
            {
                Insert(item);
            }
        }

        /// <summary>
        /// Get the item with the given id from the Store
        /// </summary>
        /// <param name="id">The item's id</param>
        /// <returns>Item with matching id</returns>
        public IStorable GetById(string id)
        {
            IStorable item;
            if (storage.TryGetValue(id, out item))
            {
                return item;
            }
            return null;
        }

        /// <summary>
        /// Returns all items in the Store in a dictionary
        /// </summary>
        /// <returns>All items in the Store in a dictionary</returns>
        public Dictionary<string, IStorable> GetAllDictionary()
        {
            var result = new Dictionary<string, IStorable>(storage);
            return result;
        }

        /// <summary>
        /// Returns all items in the Store in a list
        /// </summary>
        /// <returns>All items in the Store in a list</returns>
        public List<IStorable> GetAllList()
        {
            List<IStorable> result = new List<IStorable>();
            foreach (var item in storage)
            {
                result.Add(item.Value);
            }
            return result;
        }

        /// <summary>
        /// Sells given amount of item from the Store with the given id
        /// </summary>
        /// <param name="id">Id of the items</param>
        /// <param name="amount">How many items want to sell</param>
        /// <remarks>Throws ArgumentException in these cases
        /// <list type="bullet">
        /// <item>
        /// <description>given amount is less than 0</description>
        /// </item>
        /// <item>
        /// <description>not enough item is in stock</description>
        /// </item>
        /// </list>
        /// </remarks>
        public void Sell(string id, int amount)
        {
            if (amount < 0) throw new ArgumentException();
            foreach (var item in storage)
            {
                if (item.Key.Equals(id) && item.Value.InStock >= amount)
                {
                    item.Value.InStock -= amount;
                }
                else if (item.Key.Equals(id) && item.Value.InStock < amount) throw new ArgumentException();
            }
        }

        /// <summary>
        /// Buys the given item for the store
        /// </summary>
        /// <param name="item">Item to add to the store</param>
        public void Buy(IStorable item)
        {
            if (storage.ContainsKey(item.Id)) Buy(item.Id, item.InStock);
            else Insert(item);

        }

        /// <summary>
        /// Buys item with given id and amount for the Store
        /// </summary>
        /// <param name="id">Item's id to buy</param>
        /// <param name="amount">Amount of items to buy</param>
        public void Buy(string id, int amount)
        {
            if (amount < 0) throw new ArgumentException();
            if (storage.ContainsKey(id)) storage[id].InStock += amount;
        }

        /// <summary>
        /// Removes all items from the store with given id
        /// </summary>
        /// <param name="id">Items to remove with this id</param>
        public void Remove(string id)
        {
            storage.Remove(id);
        }

        /// <summary>
        /// Removes all of the given item from the Store
        /// </summary>
        /// <param name="item">Items to remove</param>
        public void Remove(IStorable item)
        {
            storage.Remove(item.Id, out item);
        }
    }
}
