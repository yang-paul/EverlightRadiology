using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlightRadiology
{
    public class GateSwitch : IGateSwitch, IComparable
    {
        private int leftCount, rightCount;

        public int Id { get; set; }

        public IGateSwitch Parent { get; set; }

        public GateSwitchStatus Status { get; set; }

        public GateSwitch(int Id, GateSwitchStatus status, GateSwitch parent)
        {
            this.Id = Id;
            this.Status = status;
            this.Parent = parent;
            leftCount = 0;
            rightCount = 0;
        }

        public bool IsLeftOpen()
        {
            return this.Status == GateSwitchStatus.Left;
        }

        public int LeftCount
        {
            get { return leftCount; }
        }
        public int RightCount
        {
            get { return rightCount; }
        }

        public void Toggle()
        {
            if (this.Status == GateSwitchStatus.Left)
            {
                this.leftCount++;
                this.Status = GateSwitchStatus.Right;
            }
            else
            {
                this.rightCount++;
                this.Status = GateSwitchStatus.Left;
            }
        }

        public void DisplayData()
        {
            if (Parent == null)
                Console.WriteLine("ID: {0}, Open: {1}, Balls(L,R): ({2},{3}); ", this.Id, this.Status, this.LeftCount, this.RightCount);
            else
                Console.WriteLine("ID: {0}, Open: {1}, Balls(L,R): ({2},{3}), ParentID: {4}; ", this.Id, this.Status, this.LeftCount, this.RightCount, Parent.Id);
        }

        public int CompareTo(object other)
        {
            if (other == null)
                return 1;

            GateSwitch gate = other as GateSwitch;

            if (this.Id == gate.Id)
                return 0;
            else if (Id < gate.Id)
                return -1;
            else
                return 1;
        }

        public static bool operator <(GateSwitch e1, GateSwitch e2)
        {
            return e1.CompareTo(e2) < 0;
        }

        public static bool operator >(GateSwitch e1, GateSwitch e2)
        {
            return e1.CompareTo(e2) > 0;
        }
    }
}
