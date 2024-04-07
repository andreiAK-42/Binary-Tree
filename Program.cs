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

        public void Add(T value)
        {
            Root = AddTo(Root, value);
        }

        private BinaryTreeNode<T> AddTo(BinaryTreeNode<T> Node, T Value)
        {
            if (Node == null)
            {
                return new BinaryTreeNode<T> { Value = Value };
            }

            int comparer = Comparer<T>.Default.Compare(Value, Node.Value);

            if (comparer < 0)
            {
                Node.Left = AddTo(Node.Left, Value);
            }
            else if (comparer > 0)
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

        public IEnumerable<T> ReverseOrderTraversal()
        {
            return ReverseOrderTraversal(Root);
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

        public IEnumerable<T> CentralOrderTraversal(Func<BinaryTreeNode<T>, IEnumerable<T>> OrderFunc)
        {
            return OrderFunc(Root);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> Tree = new BinaryTree<int>();

            Console.WriteLine("Введите целые числа для добавления в бинарное дерево (Для завершения введите exit):");

            string Input;

            while ((Input = Console.ReadLine()) != "exit")
            {
                if (int.TryParse(Input, out int value))
                {
                    Tree.Add(value);
                }
                else
                {
                    Console.WriteLine("Некорректный ввод! exit для выхода, либо последовательно вводите числа!");
                }
            }

            int CounterI = 0;

            foreach (var Node in Tree)
            {
                ++CounterI;

                if(CounterI == 2)
                {
                    Console.Write("\n");
                    CounterI = 0;
                }
                Console.Write(Node + " ");
            }
        }
    }
}