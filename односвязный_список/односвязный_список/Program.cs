using System.Collections;
using System.Collections.Generic;
using System;
namespace односвязный_список
{
    class Program
    {

        public class Node<T>//узел
        {
            public Node(T data)
            {
                value = data;
            }
            public T value { get; set; }
            public Node<T> Next { get; set; }
        }
        public class LinkedList<T> : IEnumerable<T>  // односвязный список
        {
            int count;
            public int Count { get { return count; } } //размер списка
            Node<T> first; // первый элемент
            Node<T> last; // последний элемент

            //AddLast(T) — добавляет новый узел, содержащий указанное значение в конец списка;
            public void AddLast(T data)
            {
                Node<T> node = new Node<T>(data);

                if (first == null)
                    first = node;
                else
                    last.Next = node;
                last = node;

                count++;
            }
            //AddFirst(T) — добавляет новый узел, содержащий указанное значение в начало списка;
            public void AppendFirst(T data)
            {
                Node<T> node = new Node<T>(data);
                node.Next = first;
                first = node;
                if (count == 0)
                {
                    last = first;
                }
                count++;
            }

            //Remove(T) — удаляет первый попавшийся узел с данным значением;
            public bool Remove(T data)
            {
                Node<T> etot = first;//текущий
                Node<T> pred = null;//предыдущий

                while (etot != null)
                {
                    if (etot.value.Equals(data))
                    {
                        // Если узел в середине или в конце
                        if (pred != null)
                        {
                            pred.Next = etot.Next;
                            if (etot.Next == null)
                                last = pred;
                        }
                        else
                        {
                            first = first.Next;

                            if (first == null)
                                last = null;
                        }
                        count--;
                        return true;
                    }
                    pred = etot;
                    etot = etot.Next;
                }
                return false;
            }
           
            //Clear — удаляет все элементы из списка;
            public void Clear()
            {
                first = null;
                last = null;
                count = 0;
            }
            //Size — возвращает число элементов списка;
            public int Size { get { return count; } }
            //Last — возвращает последний узел списка;
            public string Last()
            {
                Node<T> etot = first;//текущий
                int k = 0;

                while (k != count-1)
                {
                    etot = etot.Next;
                    k++;
                }
                return etot.value.ToString();
            }
            //First — возвращает первый узел списка;
            public string First()
            {
                Node<T> etot = first;
                return etot.value.ToString();
            }
            


            public int IndexOf(T data)//находит первый узел, содержащий указанное значение, и возвращает его позицию(иначе -1);
            {
                //int k = 0;
                int ind = -2;
                Node<T> etot = first;//текущий

                for (int k = 0; k != count; k++)
                {
                    if (etot.value.Equals(data))
                    {
                        ind = k;
                        break;
                    }
                    else
                    {

                        etot = etot.Next;
                    }

                }
                return ind+1;
            }
            // реализация интерфейса IEnumerable
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)this).GetEnumerator();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                Node<T> current = first;
                while (current != null)
                {
                    yield return current.value;
                    current = current.Next;
                }
            }
            
         
        }
        static void Main(string[] args)
        {
            LinkedList<string> linkedList = new LinkedList<string>();
            // добавление элементов
            linkedList.AddLast("Katya");
            linkedList.AddLast("Dasha");
            linkedList.AddLast("Ivan");
            linkedList.AddLast("Sveta");

            // выводим элементы
            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
                
            }
            
            Console.WriteLine();
            Console.WriteLine(linkedList.Size);
            Console.WriteLine("Первый=" + linkedList.First());
            Console.WriteLine("Последний="+linkedList.Last());
            Console.WriteLine("Позиция Dasha: " + linkedList.IndexOf("Dasha"));
            Console.WriteLine("Позиция Kris: " + linkedList.IndexOf("Kris"));
            // удаляем элемент
            linkedList.Remove("Dasha");
            foreach (var item in linkedList)
            {
                Console.Write(" "+item);
                
            }
            
            Console.WriteLine();
            Console.WriteLine(linkedList.Size);
            // добавляем элемент в начало            
            linkedList.AppendFirst("Liza");
            foreach (var item in linkedList)
            {
                Console.Write(" "+item);
                 
            }
            Console.WriteLine();

            Console.WriteLine(linkedList.Size);
            linkedList.Clear();
            Console.WriteLine("clear");

            Console.WriteLine(linkedList.Size);

            Console.ReadKey();
        }
    }
}
