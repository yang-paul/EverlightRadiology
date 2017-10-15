using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlightRadiology
{
    public interface IGateSwitch
    {
        int Id { get; set; }

        IGateSwitch Parent { get; set; }
        GateSwitchStatus Status { get; set; }

        bool IsLeftOpen();

        void Toggle();

        void DisplayData();

    }
}
