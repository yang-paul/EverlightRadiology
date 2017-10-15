using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlightRadiology
{
    public class Tree
    {
        private TreeNode root = null;
        public Tree()
        {
            root = null;
        }

        public Tree(List<TreeNode> list)
        {
            foreach (TreeNode item in list)
            {
                if (item.Level > 1)
                {
                    int parentId = item.Data.Id / 2;
                    var node = list.Where(x => x.Data.Id == parentId).SingleOrDefault();
                    if (node != null)
                    {
                        item.Data.Parent = node.Data;
                        if (node.Left == null)
                            node.Left = item;
                        else if (node.Right == null)
                            node.Right = item;
                    }

                    this.AddNode(item, node);
                }
                else
                {
                    this.AddNode(item, this.Root);
                }
                //item.DisplayNode();
            }

        }

        public TreeNode Root
        {
            get
            {
                return this.root;
            }
            set
            {
                this.root = value;
            }
        }

        public void AddNode(TreeNode newNode, TreeNode parent)
        {
            if (newNode != null)
            {
                //newNode.Data.DisplayData();

                if (this.root == null)
                {
                    this.root = newNode;
                }
                else
                if (parent == null)
                {
                    parent = newNode;
                }
                else
                {
                    if (parent.Left.Equals(newNode))
                    {
                        parent.Left = newNode;
                    }
                    else if (parent.Right.Equals(newNode))
                    {
                        parent.Right = newNode;
                    }
                }

            }

        }

        //public void Insert(ref TreeNode root, GateSwitch data)
        public void Insert(TreeNode newNode, int level)
        {
            //
            newNode.Data.DisplayData();

            if (this.root == null)
            {
                this.root = newNode;
            }
            else
            {
                TreeNode current = this.root;
                TreeNode parent;

                while (true)
                {
                    parent = current;
                    current = current.Left;
                    if (current == null)
                    {
                        newNode.Data.Status = GateSwitchStatus.Left;
                        parent.Left = newNode;
                        return;
                    }
                    else
                    {
                        current = current.Right;
                        if (current == null)
                        {
                            newNode.Data.Status = GateSwitchStatus.Right;
                            parent.Right = newNode;
                            return;
                        }
                    }


                }
            }
        }

        public TreeNode GetNodeById(TreeNode node, int id)
        {
            TreeNode child = null;
            if (id > 0 && node != null)
            {
                if (node.Data.Id == id)
                    return node;

                child = GetNodeById(node.Left, id);
                if (child == null)
                    child = GetNodeById(node.Right, id);

            }

            return child;
        }

        public bool Contains(TreeNode node, IGateSwitch data)
        {
            if (node == null) return false;

            if (node.Data == data) return true;

            Console.WriteLine(node.Data);
            if (Contains(node.Left, data))
                return true;
            else
                return Contains(node.Right, data);

            //if (node.Data > data)
            //{
            //    Console.WriteLine(node.Data);
            //    return Contains(node.Left, data);
            //}
            //else
            //{
            //    Console.WriteLine(node.Data);
            //    return Contains(node.Right, data);
            //}
        }

        public IGateSwitch FindMin(TreeNode root)
        {
            if (root == null) return null; //tree is empty

            if (root.Left == null) return root.Data; //this is the minimum

            return FindMin(root.Left);
        }


        public IGateSwitch FindMax(TreeNode root)
        {
            if (root == null) return null; //tree is empty

            if (root.Right == null) return root.Data; //this is the maximum

            return FindMax(root.Right);
        }

        public void LevelOrder(TreeNode root)
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty.");
                return;
            }

            Queue<TreeNode> printQ = new Queue<TreeNode>();

            printQ.Enqueue(root);

            while (printQ.Count > 0)
            {
                TreeNode current = printQ.Dequeue();

                Console.Write("{0} ", current.Data);

                if (current.Left != null) printQ.Enqueue(current.Left);

                if (current.Right != null) printQ.Enqueue(current.Right);
            }

            Console.WriteLine();
        }

        public void PreOrder(TreeNode root)
        {
            if (root == null)
            {
                Console.WriteLine();
                return;
            }

            Console.Write("{0} ", root.Data);

            if (root.Left != null) PreOrder(root.Left);

            if (root.Right != null) PreOrder(root.Right);
        }


        public void InOrder(TreeNode root)
        {
            if (root == null)
            {
                Console.WriteLine();
                return;
            }

            if (root.Left != null) InOrder(root.Left);

            Console.Write("{0} ", root.Data);

            if (root.Right != null) InOrder(root.Right);
        }


        public void PostOrder(TreeNode root)
        {
            if (root == null)
            {
                Console.WriteLine();
                return;
            }

            if (root.Right != null) PostOrder(root.Right);

            Console.Write("{0} ", root.Data);

            if (root.Left != null) PostOrder(root.Left);
        }

        public bool IsBinarySearchTree(TreeNode root)
        {
            if (root == null) return true;

            return IsLeftSubTreeLessOrEqual(root.Left, root.Data) && IsRightSubTreeGreater(root.Right, root.Data) && IsBinarySearchTree(root.Left) && IsBinarySearchTree(root.Right);

        }

        public bool IsLeftSubTreeLessOrEqual(TreeNode root, IGateSwitch data)
        {
            if (root == null) return true;
            Console.WriteLine("left {0} {1} {2}", root.Data, data, root.Data.Id <= data.Id);
            return root.Data.Id <= data.Id && IsLeftSubTreeLessOrEqual(root.Left, data) && IsLeftSubTreeLessOrEqual(root.Right, data);
        }

        public bool IsRightSubTreeGreater(TreeNode root, IGateSwitch data)
        {
            if (root == null) return true;

            Console.WriteLine("right {0} {1} {2}", root.Data, data, root.Data.Id > data.Id);
            return root.Data.Id > data.Id && IsRightSubTreeGreater(root.Left, data) && IsRightSubTreeGreater(root.Right, data);
        }

        public bool IsBinarySearchTreeEfficient(TreeNode node)
        {
            return IsBinarrySearchTreeEfficientInternal(node, int.MinValue, int.MaxValue);
        }

        public bool IsBinarrySearchTreeEfficientInternal(TreeNode node, int min, int max)
        {
            if (node == null) return true;

            return node.Data.Id >= min && node.Data.Id < max && IsBinarrySearchTreeEfficientInternal(node.Left, min, node.Data.Id) && IsBinarrySearchTreeEfficientInternal(node.Right, node.Data.Id, max);
        }

        public void PrintLevelTraversal()
        {
            if (root == null)
            {
                throw new Exception("Binary true is null!");
            }

            Queue<TreeNode> queueNode = new Queue<TreeNode>();
            queueNode.Enqueue(root);
            TreeNode currentNode = root;

            //Dequeue the node from left to right and put its left / right children into queue.
            while (currentNode != null && queueNode.Count > 0)
            {
                currentNode = queueNode.Dequeue();
                currentNode.DisplayNode();

                if (currentNode.Left != null)
                {
                    queueNode.Enqueue(currentNode.Left);
                }

                if (currentNode.Right != null)
                {
                    queueNode.Enqueue(currentNode.Right);
                }
            }
        }
    }
}
