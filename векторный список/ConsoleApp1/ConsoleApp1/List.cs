using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class List 
    {
        private int[] mass;

        public List()
        {
            mass = new int[0];
        }

        int count; // размер массива
        public int Count { get => count; set => count = value; }

        public void Add(int x) //добавляет объект в конец списка;
        {
            Array.Resize(ref mass, count + 1);
            mass[count++] = x;

        }

        //public void Insert(int index, int item) 
        //{
        //    Array.Resize(ref mass, count + 1);
        //    if (index > count)
        //        mass[count + 1] = item;
        //    else
        //    {
        //        for (int i = count; index < count; i--)
        //            mass[index] = item;
        //    }
        //}
        public void Insert(int index, int item)//вставляет элемент в список по указанному индексу;
        {
            Array.Resize(ref mass, count + 1);
            if (index > count)
            {
                mass[count + 1] = item;
            }
            else
            {
                for (int i = count; index < count; i--)
                {
                    mass[count - 1] = mass[count];
                }
                mass[index] = item;
            }
        }
        public void RemoveAt(int index) //удаляет элемент списка с указанным индексом;
        {
            if (count > 0)
                for (int i = index; index < count - 1; i++)
                    mass[i] = mass[i + 1];
            Array.Resize(ref mass, --count);
        }
        public int Last() //возвращает последний элемент последовательности;
        {
            return mass[count-1];
        }
        public int First() //возвращает первый элемент последовательности
        {
            return mass[0];
        }

        public void Clear() //удаляет все элементы из списка;
        {
            count = 0;
            Array.Resize(ref mass, count);
        }

        public int Size //возвращает число элементов списка;
        {
            get { return count; }
        }







    }
}
