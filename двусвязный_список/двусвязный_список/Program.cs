using System;
using System.Collections;
using System.Collections.Generic;

namespace двусвязный_список
{
    class Program
    {
        public class DoubleNode<T>
        {
            public DoubleNode(T data)
            {
                value = data;
            }
            public T value { get; set; }
            public DoubleNode<T> Pred { get; set; }
            public DoubleNode<T> Next { get; set; }
        }

        public class DoubleLinkedList<T> : IEnumerable<T>  
        {
            DoubleNode<T> first; // первый элемент
            DoubleNode<T> last; // последний элемент
            int count;  // количество элементов в списке
            public int Count { get { return count; } }

            // AddLast(T) — добавляет новый узел, содержащий указанное значение в конец списка;
            public void AddLast(T data)
            {
                DoubleNode<T> node = new DoubleNode<T>(data);

                if (first == null)
                    first = node;
                else
                {
                    last.Next = node;
                    node.Pred = last;
                }
                last = node;
                count++;
            }

            //AddFirst(T) — добавляет новый узел, содержащий указанное значение в начало списка;
            public void AddFirst(T data)
            {
                DoubleNode<T> node = new DoubleNode<T>(data);
                DoubleNode<T> etot = first;
                node.Next = etot;
                first = node;
                if (count == 0)
                    last = first;
                else
                    etot.Pred = node;
                count++;
            }
 //Insert(P, T) — вставляет новый узел в список в указанную позицию;
            //Remove(T) — удаляет первый узел из списка c указанным значением;
            public bool Remove(T data)
            {
                DoubleNode<T> etot = first;

                while (etot != null)
                {
                    if (etot.value.Equals(data))
                    {
                        break;
                    }
                    etot = etot.Next;
                }
                if (etot != null)
                {
                    // если узел не последний
                    if (etot.Next != null)
                    {
                        etot.Next.Pred = etot.Pred;
                    }
                    else//если последний
                    {
                        
                        last = etot.Pred;
                    }

                    // если узел не первый
                    if (etot.Pred != null)
                    {
                        etot.Pred.Next = etot.Next;
                    }
                    else//если первый
                    {
                        
                        first = etot.Next;
                    }
                    count--;
                    return true;
                }
                return false;
            }

            
            public bool IsEmpty { get { return count == 0; } }
            //Last — возвращает последний узел списка;
            public string Last()
            {
                DoubleNode<T> etot = first;//текущий
                int k = 0;

                while (k != count - 1)
                {
                    etot = etot.Next;
                    k++;
                }
                return etot.value.ToString();
            }
            //First — возвращает первый узел списка;
            public string First()
            {
                DoubleNode<T> etot = first;
                return etot.value.ToString();
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
            //IndexOf(T) — находит первый узел, содержащий указанное значение, и возвращает его позицию(иначе -1);
            public int IndexOf(T data)
            {
                
                int ind = -2;
                DoubleNode<T> etot = first;//текущий

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
                return ind + 1;
            }



            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)this).GetEnumerator();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                DoubleNode<T> current = first;
                while (current != null)
                {
                    yield return current.value;
                    current = current.Next;
                }
            }

            
        }
        static void Main(string[] args)
        {
            DoubleLinkedList<string> LIST = new DoubleLinkedList<string>();
            // добавление элементов
            LIST.AddLast("Katya");
            LIST.AddLast("Dasha");
            LIST.AddLast("Ivan");
            LIST.AddLast("Sveta");

            // выводим элементы
            foreach (var item in LIST)
            {
                Console.Write(" " + item);
            }
            Console.WriteLine();
            Console.WriteLine(LIST.Size);
            Console.WriteLine("Первый=" + LIST.First());
            Console.WriteLine("Последний=" + LIST.Last());
            Console.WriteLine("Позиция Dasha: " + LIST.IndexOf("Dasha"));
            Console.WriteLine("Позиция Kris: " + LIST.IndexOf("Kris"));
            // удаляем элемент
            LIST.Remove("Dasha");
            foreach (var item in LIST)
            {
                Console.Write(" " + item);

            }

            Console.WriteLine();
            Console.WriteLine(LIST.Size);
            // добавляем элемент в начало            
            LIST.AddFirst("Liza");
            foreach (var item in LIST)
            {
                Console.Write(" " + item);

            }
            Console.WriteLine();

            Console.WriteLine(LIST.Size);
            LIST.Clear();
            Console.WriteLine("clear");

            Console.WriteLine(LIST.Size);

            Console.ReadKey();

        }
    }
}
