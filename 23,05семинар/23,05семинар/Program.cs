using System;
using System.Linq;
using System.Collections.Generic;

namespace _23_05семинар
{
    public class mycomp : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x == y;
        }
        public int GetHashCode(string obj)
        {
            int h = 0;
            foreach (var i in obj)
                h += i;
            return h;
        }
    }

    public class Dict<TKey, TValue>
    {
        private struct Entry
        {
            public int hashCode;    // Lower 31 bits of hash code, -1 if unused
            public int next;        // Index of next entry, -1 if last
            public TKey key;           // Key of entry
            public TValue value;         // Value of entry
        }

        private int[] buckets;
        private Entry[] entries;
        private int count;
        private int freeList;
        private int freeCount;
        private IEqualityComparer<TKey> comparer;

        public Dict(int capacity)
        {
            comparer = EqualityComparer<TKey>.Default;
            int size = capacity;
            buckets = new int[size];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = -1;
            entries = new Entry[size];
            freeList = -1;
        }
        public Dict(int capacity, IEqualityComparer<TKey> cr)
        {
            comparer = cr;
            int size = capacity;
            buckets = new int[size];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = -1;
            entries = new Entry[size];
            freeList = -1;
        }
        private void Resize()
        {
            Resize(count * 2);
        }
        private void Resize(int newSize)
        {
            int[] newBuckets = new int[newSize];
            for (int i = 0; i < newBuckets.Length; i++)
                newBuckets[i] = -1;
            Entry[] newEntries = new Entry[newSize];
            Array.Copy(entries, 0, newEntries, 0, count);

            for (int i = 0; i < count; i++)
            {
                if (newEntries[i].hashCode >= 0)
                {
                    int bucket = newEntries[i].hashCode % newSize;
                    newEntries[i].next = newBuckets[bucket];
                    newBuckets[bucket] = i;
                }
            }
            buckets = newBuckets;
            entries = newEntries;
        }
        public void Add(TKey key, TValue value)
        {
            int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
            int targetBucket = hashCode % buckets.Length;

            for (int i = buckets[targetBucket]; i >= 0; i = entries[i].next)
            {
                if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key))
                {
                    entries[i].value = value;
                    return;
                }
            }

            int index;
            if (freeCount > 0)
            {
                index = freeList;
                freeList = entries[index].next;
                freeCount--;
            }
            else
            {
                if (count == entries.Length)
                {
                    Resize();
                    targetBucket = hashCode % buckets.Length;
                }
                index = count;
                count++;
            }
            entries[index].hashCode = hashCode;
            entries[index].next = buckets[targetBucket];
            entries[index].key = key;
            entries[index].value = value;
            buckets[targetBucket] = index;
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            int i = FindEntry(key);
            if (i >= 0)
            {
                value = entries[i].value;
                return true;
            }
            value = default(TValue);
            return false;
        }
        private int FindEntry(TKey key)
        {
            if (buckets != null)
            {
                int hashCode = comparer.GetHashCode(key) & 0x7FFFFFFF;
                for (int i = buckets[hashCode % buckets.Length]; i >= 0; i = entries[i].next)
                {
                    if (entries[i].hashCode == hashCode && comparer.Equals(entries[i].key, key))
                        return i;
                }
            }
            return -1;
        }
        public TValue this[TKey key]
        {
            get
            {
                int i = FindEntry(key);
                if (i >= 0)
                {
                    return entries[i].value;
                }
                return default(TValue);
            }
            set
            {
                Add(key, value);
            }
        }
        public void Print()
        {
            Console.WriteLine("Index\tBuckets\t\tEntries\n");
            for (int i = 0; i != buckets.Length; i++)
            {
                Console.Write(i + "\t" + buckets[i] + "\t\tKey: ");
                Console.Write(entries[i].key + ", Hash: " + entries[i].hashCode + " ");
                Console.WriteLine(entries[i].next <= 0 ? "" : (" -> " + entries[i].next));
            }
        }
        public bool ContainsKey(TKey key)
        {
            return FindEntry(key) >= 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            var dict = new Dict<string, int>(7);

            string input_text =
            System.IO.File.ReadAllText(@"big.txt");
            string[] input_check = System.IO.File.ReadAllLines(@"check.txt");
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            long elapsedMs;

            string str = "";
            foreach (var i in input_text)
            {
                if (i >= 'a' && i <= 'z'
                || i >= 'A' && i <= 'Z'
                || i == '\'')
                    str += i;
                else if (str.Length > 0)
                {
                    if (dict.ContainsKey(str))
                        ++dict[str];
                    else
                        dict[str] = 1;
                    str = "";
                }
            }

            
            Console.WriteLine("the " + dict["the"]);

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            System.Console.WriteLine("time:  " + elapsedMs);

            Console.ReadKey();
        }
    }
    
}
