using System;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            MyStack<string> mystack = new MyStack<string>();

            Console.WriteLine(mystack.Size);
            //System.Console.WriteLine(mystack.Top());

            mystack.IO = "first";
            mystack.IO = "second";

            Console.WriteLine(mystack);
            Console.WriteLine((int)mystack);

            mystack.IO = "third";
            mystack.IO = "fourth";

            while (!mystack)
            {
                Console.WriteLine(mystack.PopEx());
                // mystack.Pop();
            }

            mystack.Pop();
            Console.WriteLine(mystack);
            Console.ReadKey();
        }
    }
    class MyStack<T>
    {
        private T[] data;
        private int iter = -1;

        public int Size { get => iter + 1; }
        public T IO { get => Top(); set => Push(value); }

        public void Pop()
        {
            if (iter >= 0) --iter;
        }
        public T PopEx()
        {
            if (iter < 0) throw new Exception("Use Pop()");

            return data[iter--];
        }

        public T Top()
        {
            if (iter < 0) throw new Exception("Add check on Empty");
            return data[iter];
        }

        public void Push(T toPush)
        {
            try
            {
                data[++iter] = toPush;
            }
            catch (Exception)
            {
                Array.Resize(ref data, iter + 10);
                data[iter] = toPush;
            }
        }

        public bool Empty()
        {
            return iter < 0;
        }

        public static implicit operator bool(MyStack<T> stack) => stack.Empty();

        public static explicit operator int(MyStack<T> stack) => stack.Size;

        public static explicit operator T(MyStack<T> stack) => stack.IO;
    }
}
