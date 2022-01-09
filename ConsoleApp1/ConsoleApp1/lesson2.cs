using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class lesson2 : Ilesson
    {
        public string name => "2";

        public string description => "Задание 2. Двусвязный список";

        public void demo()
            
        {
            var constr = new linklist();
            var random = new Random();
            for (int i =0;i<20;i++)
            constr.AddNode(random.Next(10));
            Console.WriteLine($"Дабовляем 20 случайных значений");
            constr.GetCount();
            Console.WriteLine($"Дабовляем ноду после 'головы'");
            constr.AddNodeAfter(constr.head, 7);
            constr.GetCount();
            Console.WriteLine($"Удаляем ноду с индексом 10");
            constr.RemoveNode(10);
            constr.GetCount();
            Console.WriteLine($"Ищем ноду со значением 7 ");
            Console.WriteLine($"find element 7, index: {constr.FindNode(7).index}"); 
        }
        public interface ILinkedList
        {
            int GetCount(); // возвращает количество элементов в списке
            void AddNode(int value);  // добавляет новый элемент списка
            void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
            void RemoveNode(int index); // удаляет элемент по порядковому номеру
            void RemoveNode(Node node); // удаляет указанный элемент
            Node FindNode(int searchValue); // ищет элемент по его значению
        }
        public class Node
        {
            public int Value { get; set; }
            public int index { get; set; }
            public Node NextNode { get; set; }
            public Node PrevNode { get; set; }

        }
        static List<Node> nods = new List<Node>();
        public static void update()
        {
            nods.Sort(delegate (Node x, Node y)
            {
                if (x == null && y == null) return 0;
                else if (x == null) return -1;
                else if (y == null) return 1;
                else
                    return x.index.CompareTo(y.index);
            });
        }
        class linklist : ILinkedList
        {

            public Node head;
            public Node tail;
            public int length { get; set; } = 0;
            public int GetCount()
            {
                Console.WriteLine("--------------------------------------------------");
                update();
                foreach (Node node in nods)
                {
                    Console.WriteLine($"index: {node.index} value: {node.Value}");
                }
                return length;

            }

            
              public void AddNode(int value)
            {
                Node newNode = new Node();
                newNode.Value = value;
                if (tail == null)
                {
                    head = newNode;
                    length++;
                    head.index = length;
                    nods.Add(newNode);

                }
                else
                {
                    
                    newNode.PrevNode = tail;
                    tail.NextNode = newNode;
                    length++;
                    newNode.index = length;
                    nods.Add(newNode);
                }
                tail = newNode;
                update();

            }

            public void AddNodeAfter(Node node, int value)
            {
                var newNode = new Node { Value = value };
                var nextItem = node.NextNode;
                if (node.NextNode == null) tail = newNode;
                node.NextNode = newNode;
                newNode.PrevNode = node;

                newNode.NextNode = nextItem;
                
                length++;
                newNode.index = node.index+1;
                AddIndex(nextItem);
                nods.Add(newNode);
                
                update();

            }

            void AddIndex(Node node)
            {
                if (node != null)
                {
                    node.index +=1;
                    AddIndex(node.NextNode);
                }
            }
            void MinIndex(Node node)
            {
                if (node != null)
                {
                    node.index -=1;
                    MinIndex(node.NextNode);
                }
            }

            public void RemoveNode(int index)
            {
                Node node = nods.Find(item => item.index == index);
                if (node.PrevNode == null)
                {
                    
                    length--;
                    nods.Remove(node);
                    
                }
                if (node.NextNode == null)
                {
                    node.PrevNode.NextNode = null;
                    
                    length--;
                    nods.Remove(node);
                    tail = node.PrevNode;
                    
                }
                else
                {
                    node.PrevNode.NextNode = node.NextNode;
                    node.NextNode.PrevNode = node.PrevNode;
                    length--;
                    MinIndex(node.NextNode);
                    nods.Remove(node); 
                   

                }
                node = null;
                update();
            }

            public void RemoveNode(Node node)
            {
                if (node.PrevNode == null)
                {

                    length--;
                    nods.Remove(node);

                }
                if (node.NextNode == null)
                {
                    node.PrevNode.NextNode = null;

                    length--;
                    tail = node.PrevNode;
                    nods.Remove(node);

                }
                else
                {
                    node.PrevNode.NextNode = node.NextNode;
                    node.NextNode.PrevNode = node.PrevNode;
                    length--;
                    MinIndex(node.NextNode);
                    nods.Remove(node);

                }
                node = null;
                update();
            }

            public Node FindNode(int searchValue)
            {
                Node node = nods.Find(item => item.index == searchValue);
                return node;
            }


        }
    }
}
