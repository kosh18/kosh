using System;
using System.Linq;
using System.Collections.Generic;

namespace hash
{
    public class HashTable
    {
        private List<int>[] table;
        public HashTable(int size)
        {
            table = new List<int>[size];
            for (int i = 0; i != table.Length; i++)
                table[i] = new List<int>();
        }
        private int hash(int key)
        {
            return key % table.Length ;
        }
        public void Add(int key)
        {
            int index = hash(key);
            table[index].Add(key);
        }
        public void Print()
        {
            for(int i =0; i!=table.Length; i++)
            {
                Console.Write(i + " : ");
                for(int j=0; j<table[i].Count-1; j++)
                {
                    Console.Write(table[i][j] + " -> ");
                }
                if (table[i].Count > 0)
                    Console.WriteLine(table[i][table[i].Count - 1]);
                else Console.WriteLine();
            }
        }
        public bool Contains(int key)
        {
            int index = hash(key);
            return table[index].Contains(key);
        }
    }

    class Program
    {
        static void Main()
        {
            var init = new int[] { 7, 54, 20, 1, 45, 32, 10, 44 };
            var obj = new HashTable(5);
            foreach(var i in init)
            {
                obj.Add(i);
            }
            obj.Print();
            Console.WriteLine(obj.Contains(32));
            Console.WriteLine(obj.Contains(7));
            Console.WriteLine(obj.Contains(5));
            Console.ReadKey();
        }
    }
}
