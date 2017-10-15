using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlightRadiology
{
    class Program
    {
        static void Main(string[] args)
        {
            int treeLevels = 4;
            int numOfBalls = (int)Math.Pow(2, treeLevels) - 1;

            if (args.Count() == 1)
                numOfBalls = Convert.ToInt32(args[0]);

            //create a tree nodes by defined depth of tree levels
            Console.WriteLine("Create a {0} levels binary tree.", treeLevels);
            List<TreeNode> list = CreateTreeNodeList(treeLevels);

            //ceate a tree by the node list
            Tree bst = new Tree(list);

            //display initial tree status
            bst.PrintLevelTraversal();

            var ary = bst.GetNodesByTreeLevel(4);

            //predict which container will not have a ball
            Predict(treeLevels, bst);

            Console.WriteLine("");
            Console.WriteLine("Serve {0} balls in the game.", numOfBalls);

            for (var k = 0; k < numOfBalls; k++)
            {
                bst.Root.BallPasses();
            }

            //display the tree result after a set of balls served
            Console.WriteLine("Result:");
            bst.PrintLevelTraversal();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        //2. You should first initialise your system passing in the depth of the system – in this case 4. Set the state of each gate switch randomly to left or right. 
        static List<TreeNode> CreateTreeNodeList(int treeLevels)
        {
            List<TreeNode> list = new List<TreeNode>();

            for (int i = 0; i < treeLevels; i++)
            {
                int nodes = (int)Math.Pow(2, i);

                for (int j = 0; j < nodes; j++)
                {
                    //generate random gate switch status
                    GateSwitchStatus initStatus = (GateSwitchStatus)typeof(GateSwitchStatus).GetRandomEnumValue();
                    //GateSwitchStatus initStatus = (GateSwitchStatus)(i % 2);

                    TreeNode node = new TreeNode(new GateSwitch
                    (
                        j + nodes,
                        initStatus,
                        null
                    ),
                    i + 1);

                    list.Add(node);
                }
            }

            return list;
        }

        //3.1 Predict the answer to the solution i.e which container does not receive a ball.
        static void Predict(int bottomLevel, Tree bst)
        {
            var bottomNodes = bst.GetNodesByTreeLevel(bottomLevel);

            int index = 0;

            if (bottomNodes.Any())
            {
                Console.WriteLine("");
                Console.WriteLine("Counting number of gate switches closed on each path. ");

                int[] paths = new int[bottomNodes.Count() * 2];

                foreach (var node in bottomNodes)
                {
                    TreeNode current = node;
                    if (!current.Data.IsLeftOpen())
                        paths[index]++; //left path

                    if (current.Data.IsLeftOpen())
                        paths[index + 1]++; //right path

                    //counting closed gate switches
                    int count = 0;
                    while (true)
                    {
                        var parentNode = bst.GetNodeById(bst.Root, current.Data.Parent.Id);

                        if (parentNode == null)
                            break;

                        if (parentNode.Left == current && !parentNode.Data.IsLeftOpen())
                        {
                            count++;
                        }
                        else if (parentNode.Right == current && parentNode.Data.IsLeftOpen())
                        {
                            count++;
                        }

                        if (parentNode == bst.Root)
                        {
                            break;
                        }

                        current = parentNode;
                    }

                    paths[index] += count;     //left path
                    paths[index + 1] += count; //right path
                    index = index + 2;
                }

                //left first container start with A
                int charA = (int)'A';
                Console.WriteLine("Predict container {0} will not receive a ball. [container range: {1}-{2}]", (char)(charA + paths.ToList().LastIndexOf(paths.Max())), "A", (char)(charA + paths.Length - 1));
            }
        }
    }
}
