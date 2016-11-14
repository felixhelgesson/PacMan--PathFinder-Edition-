using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_pacman
{
    class HashTable
    {
        private LinkedList<object> insertionOrder = new LinkedList<object>();
        private LinkedList<Entry>[] table;
        /// <summary>
        /// The constructor of the hashtable.
        /// </summary>
        /// <param name="size"> Size of the the table</param>
        public HashTable(int size)
        {
            table = new LinkedList<Entry>[size];
            for (int i = 0; i < size; i++)
            {
                table[i] = new LinkedList<Entry>();
            }
        }
        /// <summary>
        /// Gives the key a index in the hashatable. 
        /// </summary>
        /// <param name="key">The same key wich is in the Get and Put- function.</param>
        /// <returns></returns>
        private int HashIndex(object key)
        {
            int hashCode = key.GetHashCode();
            hashCode = hashCode % table.Length;
            return (hashCode < 0) ? -hashCode : hashCode;
        }
        /// <summary>
        /// Finds the matching word(English) to the Input(Swedish).
        /// </summary>
        /// <param name="key">Input(Swedish word)</param>
        /// <returns></returns>
        public object Get(object key)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry(key, null)))
            {
                Entry entry = table[hashIndex].Find(new new_pacman.Entry(key, null)).Value;
                return entry.value;
            }
            Console.WriteLine("Can't find word: " + key);
            return null;
        }
        public int Count
        {
            get { return insertionOrder.Count; }
        }
        /// <summary>
        /// Adds input to the table. 
        /// </summary>
        /// <param name="key">(Swedish word)</param>
        /// <param name="value">(English word)</param>
        public void Put(object key, object value)
        {
            int hashIndex = HashIndex(key);

            if (!table[hashIndex].Contains(new Entry(key, null)))
            {
                Entry entry = new Entry(key, value);
                table[hashIndex].AddLast(entry);
                insertionOrder.AddLast(value);
            }
        }
        /// <summary>
        /// Removes the input in the table by the key-input(Swedish)
        /// </summary>
        /// <param name="key">(Swedish word)</param>
        public void Remove(object key)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry(key, null)))
            {
                Entry entry = new Entry(key, null);
                table[hashIndex].Remove(entry);
                Console.WriteLine(key + " is removed");
            }
        }
        public  LinkedList<object> GetInsertionOrder()
        {
            return insertionOrder;
        }
    }
}
