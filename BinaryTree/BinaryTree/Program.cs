using System.Collections;

namespace BinaryTree
{
    public class BinaryTreeNode<T>
    {
        public T Value { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }
    }

    public class BinaryTree<T> : IEnumerable<T>
    {
        private BinaryTreeNode<T> Root;

        public void Add(T Value)
        {
            Root = AddTo(Root, Value);
        }

        private BinaryTreeNode<T> AddTo(BinaryTreeNode<T> Node, T Value)
        {
            if (Node == null)
            {
                return new BinaryTreeNode<T> { Value = Value };
            }

            int Comparer = Comparer<T>.Default.Compare(Value, Node.Value);

            if (Comparer < 0)
            {
                Node.Left = AddTo(Node.Left, Value);
            }
            else if (Comparer > 0)
            {
                Node.Right = AddTo(Node.Right, Value);
            }
            return Node;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal(Root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> InOrderTraversal(BinaryTreeNode<T> Node)
        {
            if (Node != null)
            {
                foreach (var NodeTree in InOrderTraversal(Node.Left))
                {
                    yield return NodeTree;
                }

                yield return Node.Value;

                foreach (var NodeTree in InOrderTraversal(Node.Right))
                {
                    yield return NodeTree;
                }
            }
        }

        private IEnumerable<T> ReverseOrderTraversal(BinaryTreeNode<T> Node)
        {
            if (Node != null)
            {
                foreach (var NodeTree in ReverseOrderTraversal(Node.Right))
                {
                    yield return NodeTree;
                }

                yield return Node.Value;

                foreach (var NodeTree in ReverseOrderTraversal(Node.Left))
                {
                    yield return NodeTree;
                }
            }
        }

        public IEnumerator<T> GetReverseOrderEnumerator()
        {
            return ReverseOrderTraversal(Root).GetEnumerator();
        }

        private IEnumerable<BinaryTreeNode<T>> NodeInOrderTraversal(BinaryTreeNode<T> Node)
        {
            if (Node != null)
            {
                foreach (var LeftNode in NodeInOrderTraversal(Node.Left))
                {
                    yield return LeftNode;
                }

                yield return Node;

                foreach (var RightNode in NodeInOrderTraversal(Node.Right))
                {
                    yield return RightNode;
                }
            }
        }

        public IEnumerator<T> GetCustomOrderEnumerator(Func<BinaryTreeNode<T>, IEnumerable<BinaryTreeNode<T>>> OrderFunc)
        {
            return CustomOrderTraversal(Root, OrderFunc).GetEnumerator();
        }

        private IEnumerable<T> CustomOrderTraversal(BinaryTreeNode<T> Node, Func<BinaryTreeNode<T>, IEnumerable<BinaryTreeNode<T>>> OrderFunc)
        {
            if (Node != null)
            {
                if (Node.Left != null)
                {
                    foreach (var LeftNode in OrderFunc(Node.Left))
                    {
                        foreach (var LeftNodeValue in CustomOrderTraversal(LeftNode, OrderFunc))
                        {
                            yield return LeftNodeValue;
                        }
                    }
                }

                yield return Node.Value;

                if (Node.Right != null)
                {
                    foreach (var RightNode in OrderFunc(Node.Right))
                    {
                        foreach (var RightNodeValue in CustomOrderTraversal(RightNode, OrderFunc))
                        {
                            yield return RightNodeValue;
                        }
                    }
                }
            }
        }
    }

    public class Program
    {
        static void Main()
        {
            BinaryTree<object> Tree = new BinaryTree<object>();
            string Input;

            Console.WriteLine("Введите целые числа для добавления в бинарное дерево (Для завершения введите exit):");

            while ((Input = Console.ReadLine()) != "exit")
            {
                Tree.Add(Input);
            }

            int CounterI = 0;
            int CounterJ = 0;
            foreach (var Node in Tree)
            {
                ++CounterI;
                ++CounterJ;

                if (CounterI == 2)
                {
                    Console.Write("\n");
                    CounterI = 0;
                }
                Console.Write(Node + " ");
            }

            Console.Write("\nПеребор дерева:");

            var Enumerator = Tree.GetEnumerator();
            while (Enumerator.MoveNext())
            {
                Console.Write($"{Enumerator.Current} ");
            }
            
            Console.WriteLine("\nОбратное дерево:");

            var ReverseEnumerator = Tree.GetReverseOrderEnumerator();
            while (ReverseEnumerator.MoveNext())
            {
                Console.Write($"{ReverseEnumerator.Current} ");
            }

            Console.WriteLine("\nВнешний итератор :");

            var CustomOrderEnumerator = Tree.GetCustomOrderEnumerator(Node => new[] { Node.Right, Node, Node.Left });
            while (CustomOrderEnumerator.MoveNext())
            {
                Console.WriteLine(CustomOrderEnumerator.Current);
            }
        }
    }
}