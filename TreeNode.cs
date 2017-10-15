using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlightRadiology
{
    public class TreeNode
    {
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public IGateSwitch Data { get; set; }
        public int Level { get; set; }

        public TreeNode(IGateSwitch data)
        {
            this.Data = data;
            this.Level = 0;
        }
        public TreeNode(IGateSwitch data, int level)
        {
            this.Data = data;
            this.Level = level;
        }

        public bool HasSpace()
        {
            if (this.Left == null)
                return true;

            else if (this.Right == null)
                return true;

            return false;
        }

        public TreeNode AddChild(TreeNode newNode)
        {
            if (newNode != null)
            {
                if (this.Left == null)
                {
                    newNode.Data.Parent = this.Data;
                    this.Left = newNode;
                }

                if (this.Right == null)
                {
                    newNode.Data.Parent = this.Data;
                    this.Right = newNode;
                }
            }

            return newNode;
        }

        public void BallPasses()
        {
            TreeNode nextNode;
            if (this.Data.IsLeftOpen())
            {
                nextNode = this.Left;
            }
            else
            {
                nextNode = this.Right;
            }
            this.Data.Toggle();

            if(nextNode != null)
            {
                nextNode.BallPasses();
            }
        }

        public void DisplayNode()
        {
            Console.Write("Level: {0}, ", this.Level);

            if (this.Data != null)
                this.Data.DisplayData();

        }


    }
}
