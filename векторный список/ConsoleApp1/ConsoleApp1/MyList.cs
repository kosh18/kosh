using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class MyList<T>
    {

        private T[] mass;

        public MyList()
        {
            mass = new T[0];
        }

        int count; // размер массива
        public int Count { get => count; set => count = value; }

        public void Add(T x) //добавляет объект в конец списка;
        {
            Array.Resize(ref mass, count + 1);
            mass[count++] = x;

        }

        public void Insert(int index, T item) //вставляет элемент в список по указанному индексу;
        {
            Array.Resize(ref mass, count + 1);
            if (index > count)
                mass[count + 1] = item;
            else
            {
                for (int i = count; index < count; i--)
                    mass[index] = item;
            }


        }

        public void RemoveAt(int index, T item) //удаляет элемент списка с указанным индексом;
        {
            if (count > 0)
                for (int i = index; index != count - 1; i++)
                    mass[i] = mass[i - 1];
            Array.Resize(ref mass, --count);
        }
        public T Last() //возвращает последний элемент последовательности;
        {
            return mass[count];
        }
        public T First() //возвращает первый элемент последовательности
        {
            return mass[0];
        }

        public void Clear() //удаляет все элементы из списка;
        {
            count = 0;
            Array.Resize(ref mass, count);
        }

        public int size //возвращает число элементов списка;
        {
            get { return count; }
        }









    }


}
