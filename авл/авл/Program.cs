using System;

namespace авл
{


    public class Node
    {
        public int key;
        public Node left;
        public Node right;
        public int height;
        public Node(int x)
        {
            height = 1;
            key = x;
        }
    }
    public class Avl
    {
        public Node root;
        public Avl() { }
        public void Insert(int x)
        {
            root = insert(root, x);
        }
        private int height(Node p)
        {
            return p == null ? 0 : height(p.left) - height(p.right);
        }
        private int getBalance(Node p)
        {
            return p == null ? 0 : height(p.left) - height(p.right);
        }
        private Node insert(Node p, int x)
        {
            if (p == null)
                return new Node(x);
            if (x < p.key)
                p.left = insert(p.left, x);
            else
                p.right = insert(p.right, x);
            p.height = 1 + Math.Max(height(p.left), height(p.right));
            int balance = getBalance(p);
            if (balance > 1 && x < p.left.key)
                return rRot(p);
            if (balance < -1 && x > p.left.key)
                return lRot(p);
            if (balance > 1 && x > p.left.key)
            {
                p.left = lRot(p.left);
                return rRot(p);
            }
            if (balance < -1 && x < p.right.key)
            {
                p.right = rRot(p.right);
                return lRot(p);
            }
            return p;

        }
        public void Print()
        {
            print(root, 0);
        }
        private void print(Node p, int shift)
        {
            if (p.left != null)
                print(p.left, shift + 1);
            for (int i = 0; i != shift; i++)
                Console.Write(" ");
            Console.WriteLine(p.key);
            if (p.right != null)
                print(p.right, shift + 1);
        }
        public Node RotateRight(Node rt)
        {
            var piv = rt.left;
            rt.left = piv.right;
            piv.right = rt;
            return piv;
        }
        public Node RotateLeft(Node rt)
        {
            var piv = rt.right;
            rt.right = rt.left;
            piv.left = rt;
            return piv;
        }
        public Node rRot(Node y)
        {
            var x = y.left;
            var T2 = x.right;
            x.right = y;
            y.left = T2;
            y.height = Math.Max(height(y.left), height(y.right)) + 1;
            x.height = Math.Max(height(x.left), height(x.right)) + 1;
            return x;
        }
        public Node lRot(Node x)
        {
            var y = x.right;
            var T2 = y.left;
            y.left = x;
            x.right = T2;
            y.height = Math.Max(height(y.left), height(y.right)) + 1;
            x.height = Math.Max(height(x.left), height(x.right)) + 1;
            return y;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new Avl();
            var rnd = new System.Random(1);
            int[] init = { 5, 6, 7, 8, 9, 10, 11, 12, 4, 3, 2, 1, 0 };
            foreach (var i in init)
                obj.Insert(i);
            obj.Print();
        }
    }

}

