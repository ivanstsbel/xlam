

using System;
using System.Collections.Generic;
using System.Text;



namespace ConsoleApp1
{
    internal class lesson4 : Ilesson
    {
        public string name => "4";

        public string description => "Задание 4. Двоичное дерево поиска";

        public void demo()
        {

            program.task();

        }
    }

    public static class program
    {
        static List<Node<int>> nodes = new List<Node<int>>();

        public static void task()
        {
            Node<int> newNod = new Node<int>();
            var inter = new intr();
            try
            {
                while (true)
                {

                    Console.WriteLine("Введите команду(help - для помощи):");
                    string comm = Console.ReadLine();
                    if (comm == "add") inter.AddItem(newNod);
                    if (comm == "print") inter.PrintTree(newNod);
                    if (comm == "remove") inter.RemoveItem(newNod);
                    if (comm == "find") inter.GetNodeByValue(newNod);
                    if (comm == "help")
                    {
                        Console.WriteLine("add  добавить значение \nprint вывести дерево на экран \nremove удалить значение \nfind найти значение");
                    }

                }
            }
            catch
            {
                Console.WriteLine("error!");
            }
        }


        static Node<int> getNodeByValue(Node<int> root, int value)
        {
            while (root!=null)
            {
                if (root.Data> value)
                {
                    root = root.Left;
                    continue;
                }
                else if (root.Data<value)
                {
                    root = root.Right;
                    continue;
                }
                else
                {
                    return root;
                }
            }
            return null;
        }
        static Node<int> getMaxNode(Node<int> root)
        {
            while (root.Right!=null)
            {
                root = root.Right;
            }
            return root;
        }
        static void deleteValue(Node<int> root, int value)
        {
            Node<int> target = getNodeByValue(root, value);
            removeNodeByPtr(target);
        }
        static void removeNodeByPtr(Node<int> target)
        {
            if ((target.Left !=null)&& (target.Right != null))
            {
                Node<int> localMax = getMaxNode(target.Left);
                target.Data = localMax.Data;
                removeNodeByPtr(localMax);
                return;
            }
            else if (target.Left!=null)
            {
                if (target == target.Parent.Left)
                {
                    target.Parent.Left = target.Left;
                }
                else
                {
                    target.Parent.Right = target.Left;
                }
            }
            else if (target.Right!=null)
            {
                if (target == target.Parent.Right)
                {
                    target.Parent.Right = target.Right;
                }
                else
                {
                    target.Parent.Left = target.Right;
                }
            }
            else
            {
                if (target == target.Parent.Left)
                {
                    target.Parent.Left = null;
                }
                else
                {
                    target.Parent.Right = null;
                }
            }
           
        }
        public static void Print(this Node<int> root, string textFormat = "", int spacing = 2, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { node = next, Text = next.Data.ToString(textFormat) };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + spacing;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.node.Left)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                    }
                }
                next = next.Left ?? next.Right;
                for (; next == null; item = item.Parent)
                {
                    int top = rootTop + 2 * level;
                    Print(item.Text, top, item.StartPos);
                    if (item.Left != null)
                    {
                        Print("/", top + 1, item.Left.EndPos);
                        Print("_", top, item.Left.EndPos + 1, item.StartPos);
                    }
                    if (item.Right != null)
                    {
                        Print("_", top, item.EndPos, item.Right.StartPos - 1);
                        Print("\\", top + 1, item.Right.StartPos - 1);
                    }
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos + 1;
                        next = item.Parent.node.Right;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos - 1;
                        else
                            item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }
        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }
        public static Node<int> Insert(Node<int> head, int value)
        {
            Node<int> tmp = null;
            if (head == null)
            {
                head = new Node<int>();
                head = GetFreeNode(value, null);
                return head;

            }

            tmp = head;
            while (tmp != null)
            {
                if (value > tmp.Data)
                {
                    if (tmp.Right != null)
                    {
                        tmp = tmp.Right;
                        continue;
                    }
                    else
                    {
                        tmp.Right = GetFreeNode(value, tmp);
                        return head;
                    }
                }
                else if (value <= tmp.Data)
                {
                    if (tmp.Left != null)
                    {
                        tmp = tmp.Left;
                        continue;
                    }
                    else
                    {
                        tmp.Left = GetFreeNode(value, tmp);
                        return head;
                    }
                }
                else
                {
                    throw new Exception("Дерево построено неправильно");                  // Дерево построено неправильно
                }
            }
            return head;
        }
        public static Node<int> GetFreeNode(int value, Node<int> parent)
        {
            Node<int> tmp = new Node<int>();
            tmp.Left = tmp.Right = null;
            tmp.Data = value;
            tmp.Parent = parent;
            //Console.WriteLine($"{tmp.Data} {parent}");
            return tmp;

        }
        public class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Left { get; set; }
            public Node<T> Right { get; set; }
            public Node<T> Parent { get; set; }
        }
        public class TreeNode
        {
            public int Value { get; set; }
            public TreeNode LeftChild { get; set; }
            public TreeNode RightChild { get; set; }

            public override bool Equals(object obj)
            {
                var node = obj as TreeNode;

                if (node == null)
                    return false;

                return node.Value == Value;
            }
        }

        public interface ITree
        {
            TreeNode GetRoot();
            void  AddItem(Node<int> nod); // добавить узел
            void RemoveItem(Node<int> nod); // удалить узел по значению
            Node<int> GetNodeByValue(Node<int> nod); //получить узел дерева по значению
            void PrintTree(Node<int> nod); //вывести дерево в консоль
        }
        public class intr : ITree
        {
            public  void AddItem(Node<int> node)
            {
                
                Console.WriteLine("Введите значение :");
                string tmp = Console.ReadLine();
                int x;

                if (int.TryParse(tmp, out x))
                {
                    if (node.Data == 0 && node.Parent == null)
                    
                        node.Data = x;

                    
                   else node = Insert(node, x);
                        }
                else Console.WriteLine("Ошибка! введите число");

            }

            public Node<int> GetNodeByValue(Node<int> nod)
            {
                Console.WriteLine("Введите значение :");
                string tmp = Console.ReadLine();
                int x;
                int.TryParse(tmp, out x);
                var find = getNodeByValue(nod,x);
                return find;



            }

            public TreeNode GetRoot()
            {
                throw new NotImplementedException();
            }

            public void PrintTree(Node<int> nod)
            {
                Print(nod);
            }

            public void RemoveItem(Node<int> nod)
            {
                Console.WriteLine("Введите значение для удаления :");
                string tmp = Console.ReadLine();
                int x;
                int.TryParse(tmp, out x);
                deleteValue(nod, x);


            }
        }

        public static class TreeHelper
        {
            public static NodeInfo[] GetTreeInLine(ITree tree)
            {
                var bufer = new Queue<NodeInfo>();
                var returnArray = new List<NodeInfo>();
                var root = new NodeInfo() { Node = tree.GetRoot() };
                bufer.Enqueue(root);

                while (bufer.Count != 0)
                {
                    var element = bufer.Dequeue();
                    returnArray.Add(element);

                    var depth = element.Depth + 1;

                    if (element.Node.LeftChild != null)
                    {
                        var left = new NodeInfo()
                        {
                            Node = element.Node.LeftChild,
                            Depth = depth,
                        };
                        bufer.Enqueue(left);
                    }
                    if (element.Node.RightChild != null)
                    {
                        var right = new NodeInfo()
                        {
                            Node = element.Node.RightChild,
                            Depth = depth,
                        };
                        bufer.Enqueue(right);
                    }
                }

                return returnArray.ToArray();
            }
        }



        public class NodeInfo
        {
            public Node<int> node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
            public int Depth { get; set; }
            public TreeNode Node { get; set; }

        }
    }
}
