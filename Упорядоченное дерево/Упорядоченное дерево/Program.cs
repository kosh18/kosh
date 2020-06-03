using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Упорядоченное_дерево
{
    class Program
    {
        public class Node<T> where T : IComparable
        {
            public T value;
            public Node<T> left;
            public Node<T> right;
            public Node(T x)
            {
                value = x;
            }
        }

        public class Tree<T> where T : IComparable
        {
             Node<T> root;

            public bool IsEmpty() //пустое?
            {
                if (root == null) return true;
                else return false;
            }

            public void Add(T Value) //добавляет значение
            {

                if (root == null)
                {
                    root = new Node<T>(Value);
                    return;
                }

                if (root.value.CompareTo(Value) > 0)
                {
                    if (root.left != null) add(Value, root.left);
                    else root.left = new Node<T>(Value);
                }

                if (root.value.CompareTo(Value) < 0)
                {
                    if (root.right != null) add(Value, root.right);
                    else root.right = new Node<T>(Value);
                }
            }
            
            private void add(T Value, Node<T> node)
            {
                if (node.value.CompareTo(Value) > 0)
                {
                    if (node.left != null) add(Value, node.left);
                    else node.left = new Node<T>(Value);
                }

                if (node.value.CompareTo(Value) < 0)
                {
                    if (node.right != null) add(Value, node.right);
                    else node.right = new Node<T>(Value);
                }
            }

            public void Remove(T x) //удаляет указанное значение
            {
                if (root.value.CompareTo(x) == 0)
                {
                    Node<T> doproot = new Node<T>(x);
                    doproot.right = root;
                    root = doproot;
                    remove(x, root);
                    root = root.right;
                }
                else remove(x, root);
            }
            private void remove(T Value, Node<T> node)
            {

                if (node == null) return;
                Node<T> doptree = null;

                if (node.left != null && node.left.value.CompareTo(Value) == 0)
                {
                    if (node.left.left == null && node.left.right == null)
                    {
                        node.left = null;
                        return;
                    }

                    if (node.left.left == null) doptree = node.left.right;
                    if (node.left.right == null) doptree = node.left.left;
                    if (doptree != null)
                    {
                        node.left = doptree;
                        return;
                    }

                    doptree = node.left.left;
                    node.left = node.left.right;

                    Node<T> min = node.left;
                    while (min.left != null) min = min.left;
                    min.left = doptree;
                    return;

                }

                if (node.right != null && node.right.value.CompareTo(Value) == 0)
                {
                    if (node.right.left == null && node.right.right == null)
                    {
                        node.right = null;
                        return;
                    }

                    if (node.right.left == null) doptree = node.right.right;
                    if (node.right.right == null) doptree = node.right.left;
                    if (doptree != null)
                    {
                        node.right = doptree;
                        return;
                    }

                    doptree = node.right.left;
                    node.right = node.right.right;

                    Node<T> min = node.right;
                    while (min.left != null) min = min.left;
                    min.left = doptree;
                    return;
                }

                if (node.value.CompareTo(Value) > 0) remove(Value, node.left);
                if (node.value.CompareTo(Value) < 0) remove(Value, node.right);
            }

            public int GetHeight()//высота дерева
            {
                return getheight(root);
            }
           
            private int getheight(Node<T> node)
            {
                if (node == null) return 0;
                return 1 + Math.Max(getheight(node.left), getheight(node.right));
            }

            public Node<T> Find(T Value)// находит TreeNode с указаным значением
            {
                if (IsEmpty()) return null;
                if (root.value.CompareTo(Value) == 0) return root;
                if (root.value.CompareTo(Value) > 0) return find(Value, root.left);
                return find(Value, root.right);
            }
            
            private Node<T> find(T Value, Node<T> node)
            {
                if (node == null) return null;
                if (node.value.CompareTo(Value) == 0) return node;
                if (node.value.CompareTo(Value) > 0) return find(Value, node.left);
                return find(Value, node.right);
            }

            public int LeafCount()//кол-во листьев
            {
                return leafcount(root);
            }

         
            private int leafcount(Node<T> node)
            {
                if (node == null) return 0;
                if (node.left == null && node.right == null) return 1;
                return leafcount(node.left) + leafcount(node.right);
            }
        }


        static void Main(string[] args)
        {
            
        }
    }
}
